using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Public.User
{
    public class UserAddAtTeamFromRequestDto
    {
        public long UserId { get; set; }
        public long TeamId { get; set; }
        public string RoleName { get; set; }
    }
}
