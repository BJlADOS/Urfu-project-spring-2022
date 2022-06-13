using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.Team;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Features.Admin.User.Query
{
    public class FreeUsersGetQuery : IRequest<ICollection<ProfileUserDto>>
    {
        public string Term { get; set; }
    }

    public class FreeUsersGetQueryHandler : IRequestHandler<FreeUsersGetQuery, ICollection<ProfileUserDto>>
    {
        private readonly IReadOnlyRepository<Core.Domain.Model.User.User> _userRepository;
        private readonly IMapper _mapper;
        private IUserProfileProvider _profileProvider;

        public FreeUsersGetQueryHandler(IReadOnlyRepository<Core.Domain.Model.User.User> userRepository, IMapper mapper, IUserProfileProvider profileProvider)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _profileProvider = profileProvider;
        }

        public async Task<ICollection<ProfileUserDto>> Handle(FreeUsersGetQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ListAsync(UserSpecification.GetFree(request.Term, _profileProvider.GetProfile().User.EventId), cancellationToken);

            var userDtos = new List<ProfileUserDto>();

            foreach (var user in users)
            {
                var competencies = _mapper.Map<ICollection<UserCompetencyDto>>(user.Competencies);
                var team = _mapper.Map<TeamDto>(user.Team);
                var userDto = _mapper.Map<ProfileUserDto>(user);
                userDto.Competencies = competencies;
                userDto.Team = team;
                userDtos.Add(userDto);
            }

            return userDtos;
        }
    }
}
