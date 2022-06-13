using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Features.Public.User.GetProfile
{
    public class UserGetProfileQuery : IRequest<ProfileUserDto>
    {
        public long Id { get; set; }
    }
}
