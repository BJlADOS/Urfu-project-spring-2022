using Workshop.Core.Domain.Model.Session;
using Workshop.Core.Domain.Model.User;

namespace Workshop.Core.Services.Security
{
    public class UserProfile
    {
        public User User { get; set; }
        public Session Session { get; set; }
    }
}
