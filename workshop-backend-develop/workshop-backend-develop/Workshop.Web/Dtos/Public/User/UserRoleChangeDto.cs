using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Public.User
{
    public class UserRoleChangeDto
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }
}
