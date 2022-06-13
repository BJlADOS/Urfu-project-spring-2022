using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model;
using Workshop.Core.Domain.Model.Session;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services;
using Workshop.Core.Services.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Workshop.Core.Domain.Model.ApiKey;

namespace Workshop.Web.WebSecurity
{
    public class AuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly IUserProfileProvider _userProfileProvider;
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IApiKeyRepository _apiKeyRepository;

        public AuthorizationFilter(IUserProfileProvider userProfileProvider,
            IUserRepository userRepository,
            ISessionRepository sessionRepository, IApiKeyRepository apiKeyRepository)
        {
            _userProfileProvider = userProfileProvider;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _apiKeyRepository = apiKeyRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.OnStarting(UpdateCookies, context.HttpContext);

            if (context.Filters.Any(x => x is IAllowAnonymousFilter))
            {
                return;
            }

            var cancellationToken = CancellationToken.None;

            if (context.HttpContext.Request.Cookies.TryGetSid(out Guid sid))
            {
                var session =
                    await _sessionRepository.SingleOrDefaultAsync(SessionSpecification.GetSession(new SessionId(sid)),
                        cancellationToken);
                if (session != null)
                {
                    var user = await _userRepository.SingleAsync(UserSpecification.GetById(session.UserId),
                        cancellationToken);
                    if (Authorize(user))
                    {
                        if (!HavePermission(context, user))
                        {
                            context.Result = new UnauthorizedResult();
                            return;
                        }

                        var userProfile = new UserProfile()
                        {
                            User = user,
                            Session = session,
                        };

                        _userProfileProvider.SetProfile(userProfile);

                        return;
                    }
                }

                if (context.HttpContext.Request.Headers.TryGetValue("API_KEY", out var extractedApiKey))
                {
                    var apiKey =
                        await _apiKeyRepository.SingleAsync(ApiKeySpecification.GetByKeyString(extractedApiKey),
                            cancellationToken);
                    var user = new User($"ApiUser{apiKey.UserType}-{apiKey.Name}", "", "", apiKey.UserType,
                        apiKey.EventId);

                    if (!HavePermission(context, user))
                    {
                        context.Result = new UnauthorizedResult();
                        return;
                    }

                    var userProfile = new UserProfile()
                    {
                        User = user,
                        Session = session,
                    };

                    _userProfileProvider.SetProfile(userProfile);

                    return;
                }
            }

            context.Result = new UnauthorizedResult();
        }

        private bool HavePermission(AuthorizationFilterContext context, User user)
        {
            var path = context.HttpContext.Request.Path.Value;
            // Администратору доступны все методы API
            if (user.UserType == UserType.Admin)
                return true;
            // Для каждой роли доступны только соответсвующие ей методы
            if (path.Contains("admin"))
                return user.UserType == UserType.Admin;
            if (path.Contains("expert"))
                return user.UserType == UserType.Expert;
            if (path.Contains("teamlead"))
                return user.UserType == UserType.Teamlead;
            return true;
        }

        private Task UpdateCookies(object arg)
        {
            var httpContext = (HttpContext) arg;
            var userProfile = _userProfileProvider.GetProfile();

            if (userProfile != null)
            {
                var session = userProfile.Session;
                httpContext.Response.Cookies.SetSid(session.Key.Id, session.Expires);
            }

            return Task.CompletedTask;
        }

        private bool Authorize(User user)
        {
            //todo: Rules for calculate user permissions

            return true;
        }
    }
}