using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Features.Public.User.Query
{
    public class UserGetQuery : IRequest<UserDto>
    {
        public long Id { get; set; }
    }
}
