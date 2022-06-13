using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using Microsoft.Extensions.Configuration;
using Workshop.Core.Domain.Model.UrfuData;

namespace Workshop.Web.Features.Public.HTML
{
    public class HtmlCommands
    {
        private readonly string _userLogin;
        private readonly string _userPassword;
        private readonly Uri _requestUri;
        private readonly HttpClient _client;

        public HtmlCommands(string userLogin, string userPassword, IConfiguration configuration)
        {
            _userLogin = userLogin;
            _userPassword = userPassword;
            _client = new HttpClient(new HttpClientHandler {CookieContainer = new CookieContainer()});
            _requestUri = GetAuthResponseRequestUri(configuration).Result;
        }

        private async Task<Uri> GetAuthResponseRequestUri(IConfiguration Configuration)
        {
            var uri = Configuration["UrfuRequest:AuthUri"];
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,
                    string>("UserName",
                    _userLogin),
                new KeyValuePair<string,
                    string>("Password",
                    _userPassword),
                new KeyValuePair<string,
                    string>("AuthMethod",
                    "FormsAuthentication")
            });
            var authResponse = await _client.SendAsync(request).ConfigureAwait(false);
            authResponse.EnsureSuccessStatusCode();

            return authResponse.RequestMessage.RequestUri;
        }

        public bool IsAuth() => _requestUri?.Query?.Contains("ok") ?? throw new InvalidOperationException();

        public async Task<UrfuUserData> GetUrfuUserData()
        {
            if (!IsAuth())
                throw new ArgumentException("неправильный логнин или пароль");

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, _requestUri);
            var httpResponseMessage = await _client.SendAsync(requestMessage).ConfigureAwait(false);
            var htmlWithUserDataAsString = await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

            var parser = new HtmlParser();
            var document = parser.ParseDocument(htmlWithUserDataAsString);

            var data = document
                .QuerySelectorAll("div.myself")
                .FirstOrDefault()
                ?.TextContent
                .Split('\n', '\t')
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();

            var fio = data[0].Split();
            var academicGroup = data[2].Split(':')[1].Trim().Split(", ").Last();
            var email = data[3].Split(':')[1].Trim();

            return new UrfuUserData(email, fio[0], fio[1], fio[2], academicGroup);
        }
    }
}