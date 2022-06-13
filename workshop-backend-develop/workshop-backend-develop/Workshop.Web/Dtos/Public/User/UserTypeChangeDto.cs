using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.User;

namespace Workshop.Web.Dtos.Public.User
{
    public class UserTypeChangeDto
    {
        public long Id { get; set; }

        public UserType Type { get; set; }
    }
}
