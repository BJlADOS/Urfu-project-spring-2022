using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using MediatR;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Helpers;
using Workshop.Web.Dtos.Public.User;
using Workshop.Web.Exceptions;
using Workshop.Web.Features.Public.User.Generate;

namespace Workshop.Web.Features.Admin.GenerateUser
{
    public class UsersGenerateCommandHandler : IRequestHandler<UsersGenerateCommand, byte[]>
    {
        private readonly Regex _regex = new Regex(@"(.+)-(\*+)");
        private const string Numbers = "0123456789";

        private const string Chars =
            "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private readonly Random _random;

        private readonly IUserRepository _userRepository;

        public UsersGenerateCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _random = new Random();
        }

        public async Task<byte[]> Handle(UsersGenerateCommand request,
                                         CancellationToken cancellationToken)
        {
            if (!_regex.IsMatch(request.Parameters.Template))
            {
                throw new ConflictException("Template should have format {prefix}-{**}");
            }

            if (request.Parameters.Count > 100)
            {
                throw new ConflictException("User count should not be greater than 100");
            }
            
            var template = _regex.Match(request.Parameters.Template);
            var prefix = template.Groups[1].Value;
            var symbolsCount = template.Groups[2].Value.Length;

            var existUsers = await _userRepository.ListAsync(cancellationToken);
            var existLogins = existUsers
                              .Select(x => x.Login)
                              .ToHashSet();
            var userLoginDtos = new List<UserLoginDto>();
            var users = new List<Core.Domain.Model.User.User>();

            for (var i = 0; i < request.Parameters.Count; i++)
            {
                var login = GenerateUserLogin(prefix, symbolsCount, existLogins);
                userLoginDtos.Add(login);
                existLogins.Add(login.Login);
                var salt = GenerateRandomString(symbolsCount);
                var hashedPassword =
                    CryptographyHelper.GenerateMD5Hash(salt +
                                                       CryptographyHelper
                                                           .GenerateMD5Hash(login.Password));
                var user = new Core.Domain.Model.User.User(login.Login, hashedPassword, salt,
                                                           request.Parameters.UserType,
                                                           request.Parameters.EventId);
                if (user.UserType == UserType.Admin || user.UserType == UserType.Expert)
                {
                    user.UpdateProfileFilled(true);
                }

                users.Add(user);
            }

            await _userRepository.AddRangeAsync(users, cancellationToken);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            var wb = GetWorkbook(userLoginDtos);
            var stream = new MemoryStream();
            wb.Save(stream, new XlsSaveOptions(SaveFormat.Xlsx));
            return stream.ToArray();
        }

        private Workbook GetWorkbook(List<UserLoginDto> userLoginDtos)
        {
            var result = new Workbook();
            var sheet = result.Worksheets[0];
            var cells = sheet.Cells;

            cells["A1"].PutValue("Логин");
            cells["B1"].PutValue("Пароль");

            for (var i = 0; i < userLoginDtos.Count; i++)
            {
                cells[$"A{i + 2}"].PutValue(userLoginDtos[i].Login);
                cells[$"B{i + 2}"].PutValue(userLoginDtos[i].Password);
            }

            return result;
        }

        private UserLoginDto GenerateUserLogin(string prefix, int symbolsCount,
                                               HashSet<string> existLogins)
        {
            var newLogin = $"{prefix}-{GenerateRandomNumeric(symbolsCount)}";
            while (existLogins.Contains(newLogin))
            {
                newLogin = $"{prefix}-{GenerateRandomNumeric(symbolsCount)}";
            }

            return new UserLoginDto
                   {
                       Login = newLogin,
                       Password = GenerateRandomString(6)
                   };
        }

        private string GenerateRandomNumeric(int length)
        {
            return GenerateRandomStringFrom(Numbers, length);
        }

        private string GenerateRandomString(int length)
        {
            return GenerateRandomStringFrom(Chars, length);
        }

        private string GenerateRandomStringFrom(string alphabet, int lenght)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < lenght; i++)
            {
                builder.Append(alphabet[_random.Next(alphabet.Length)]);
            }

            return builder.ToString();
        }
    }
}