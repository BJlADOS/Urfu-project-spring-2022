using Workshop.Core.Domain.Model.User;

namespace Workshop.Web.Dtos.Public.User
{
    public class UserGenerationParametersDto
    {
        public long EventId { get; set; }
        public int Count { get; set; }
        public UserType UserType { get; set; }
        public string Template { get; set; }
    }
}
