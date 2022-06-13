using MediatR;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Features.Public.User.Generate
{
    public class UsersGenerateCommand : IRequest<byte[]>
    {
        public UserGenerationParametersDto Parameters { get; set; }
    }
}
