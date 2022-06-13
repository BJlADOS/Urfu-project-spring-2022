using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.Project;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Features.Admin.User.Query
{
    public class UserGetQuery : IRequest<UserDetailedDto>
    {
        public long Id { get; set; }
    }

    public class UserGetQueryHandler : IRequestHandler<UserGetQuery, UserDetailedDto>
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserGetQueryHandler(UserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDetailedDto> Handle(UserGetQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.DetailedUserInfo(UserSpecification.GetById(request.Id), cancellationToken);

            var userDto = _mapper.Map<UserDetailedDto>(user);
            userDto.Project = _mapper.Map<ShortProjectDto>(user.Team?.Project);

            return userDto;
        }
    }
}
