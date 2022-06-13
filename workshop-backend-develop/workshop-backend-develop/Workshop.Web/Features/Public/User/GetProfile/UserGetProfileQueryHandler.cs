using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Expert.User;
using Workshop.Web.Dtos.Public.Auditorium;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.Project;
using Workshop.Web.Dtos.Public.Team;
using Workshop.Web.Dtos.Public.User;
using Workshop.Web.Features.Public.User.Query;

namespace Workshop.Web.Features.Public.User.GetProfile
{
    public class UserGetProfileQueryHandler : IRequestHandler<UserGetProfileQuery, ProfileUserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;
        private readonly IReadOnlyRepository<Core.Domain.Model.Event.Event> _eventRepository;
        private readonly IReadOnlyRepository<Core.Domain.Model.Team.Team> _teamRepository;
        private IReadOnlyRepository<Core.Domain.Model.Auditorium.Auditorium> _auditoriumRepository;
        private IUserProfileProvider _profileProvider;
        private readonly IMapper _mapper;
        


        public UserGetProfileQueryHandler(IUserRepository userRepository, IMapper mapper,
            IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository,
            IUserProfileProvider profileProvider,
            IReadOnlyRepository<Core.Domain.Model.Auditorium.Auditorium> auditoriumRepository, TeamRepository teamRepository, IReadOnlyRepository<Core.Domain.Model.Event.Event> eventRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _projectRepository = projectRepository;
            _profileProvider = profileProvider;
            _auditoriumRepository = auditoriumRepository;
            _teamRepository = teamRepository;
            _eventRepository = eventRepository;
        }

        public async Task<ProfileUserDto> Handle(UserGetProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleAsync(UserSpecification.GetById(request.Id), cancellationToken);
 
            
            var userDto = _mapper.Map<ProfileUserDto>(user);

            if (user.Team != null)
            {
               

               

                var project = await _projectRepository.SingleAsync(
                    ProjectSpecification.GetById(user.Team.ProjectId, _profileProvider.GetProfile().User.EventId),
                    cancellationToken);

                var fillTeams = await _teamRepository.LongCountAsync(TeamSpecification.GetFillTeams(project.Key, user.EventId), cancellationToken);

                var teamDto = _mapper.Map<TeamDto>(user.Team);
                teamDto.Project = _mapper.Map<ShortProjectDto>(project);

                teamDto.Project.FillTeamsCount = (int)fillTeams;

                // if (user.Team.AuditoriumId.HasValue)
                // {
                //     var auditorium = await _auditoriumRepository.SingleAsync(
                //         AuditoriumSpecification.GetById(user.Team.AuditoriumId.Value,
                //             _profileProvider.GetProfile().User.EventId), cancellationToken);
                //     teamDto.Auditorium = _mapper.Map<AuditoriumDto>(auditorium);
                // }

                if (user.Team.ExpertId.HasValue)
                {
                    var expert = await _userRepository.SingleAsync(UserSpecification.GetById(user.Team.ExpertId.Value),
                        cancellationToken);

                    teamDto.Expert = _mapper.Map<ExpertUserDto>(expert);
                }

                var users = await _userRepository.ListAsync(
                    UserSpecification.GetByTeam(user.TeamId, _profileProvider.GetProfile().User.EventId),
                    cancellationToken);

                teamDto.Users = _mapper.Map<ICollection<UserDto>>(users);

                //var @event = await _eventRepository.SingleAsync(EventSpecification.GetById(_profileProvider.GetProfile().User.EventId),
                //    cancellationToken);

                userDto.Team = teamDto;
            }

            return userDto;
        }
    }
}