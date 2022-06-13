using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Features.Public.User.Update
{
    public class UserUpdateCommand : IRequest
    {
        public UserDto User { get; set; }
    }
}
