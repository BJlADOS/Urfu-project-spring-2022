using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.User;

namespace Workshop.Web.Dtos.Public.User
{
    public class CurrentUserTypeChange
    {
        public UserType Type { get; set; }
    }
}
