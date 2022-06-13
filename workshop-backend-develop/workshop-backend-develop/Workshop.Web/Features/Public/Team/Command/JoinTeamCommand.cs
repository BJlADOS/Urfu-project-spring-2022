using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.Project.Command
{
    public class JoinTeamCommand : IRequest
    {
        public long TeamId { get; set; }
        public long RoleId { get; set; }
    }

    public class JoinTeamCommandHandler : AsyncRequestHandler<JoinTeamCommand>
    {
        private IProjectRepository _projectRepository;
        private IReadOnlyRepository<Core.Domain.Model.Auditorium.Auditorium> _auditoriumRepository;
        private IReadOnlyRepository<Core.Domain.Model.Event.Event> _eventRepository;
        private IReadOnlyRepository<Core.Domain.Model.Role.Role> _roleRepository;
        private IUserRepository _userRepository;
        private ITeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public JoinTeamCommandHandler(ITeamRepository teamRepository,
            IUserRepository userRepository,
            IProjectRepository projectRepository,
            IReadOnlyRepository<Core.Domain.Model.Auditorium.Auditorium> auditoriumRepository,
            IUserProfileProvider profileProvider, IReadOnlyRepository<Core.Domain.Model.Role.Role> roleRepository,
            IReadOnlyRepository<Core.Domain.Model.Event.Event> eventRepository)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _auditoriumRepository = auditoriumRepository;
            _profileProvider = profileProvider;
            _roleRepository = roleRepository;
            _eventRepository = eventRepository;
        }

        protected override async Task Handle(JoinTeamCommand request, CancellationToken cancellationToken)
        {
            var currentEvent = await _eventRepository.SingleOrDefaultAsync(
                EventSpecification.GetById(_profileProvider.GetProfile().User.EventId),
                cancellationToken);

            var team = await _teamRepository.SingleOrDefaultAsync(
                TeamSpecification.GetById(request.TeamId, _profileProvider.GetProfile().User.EventId),
                cancellationToken);

            var project = await _projectRepository.SingleOrDefaultAsync(
                ProjectSpecification.GetById(team.ProjectId, _profileProvider.GetProfile().User.EventId),
                cancellationToken);

            var role = await _roleRepository.SingleOrDefaultAsync(
                RoleSpecification.GetById(request.RoleId),
                cancellationToken);

            var user = await _userRepository.SingleOrDefaultAsync(
                UserSpecification.GetById(_profileProvider.GetProfile().User.Key),
                cancellationToken);

            if (team == null || project == null || user == null || currentEvent == null)
            {
                throw new NotFoundException();
            }
            if (!project.IsAvailable)
            {
                throw new ConflictException();
            }
            if (user.TeamId != null)
            {
                throw new ForbiddenException();
            }

            user.UpdateTeam(team.Key);
            user.UpdateRole(request.RoleId);

            //Комплектуем команду, если она полная
            if (team.Users != null && team.Users.Count >= project.TeamCapacity - 1)
            {
                // team.StartTest();
                //TODO:
                team.CompleteTest();
             
                // team.UpdateTeamCompleteDate(DateTimeHelper.GetCurrentTime() + currentEvent.TestDuration);
                team.UpdateTeamCompleteDate(DateTimeHelper.GetCurrentTime());

                // var availableAuditoriums = await _auditoriumRepository.ListAsync(
                //     AuditoriumSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);
                // var teamsWithAuditoriums = await _teamRepository.ListAsync(
                //     TeamSpecification.GetTeamsWithAuditorium(_profileProvider.GetProfile().User.EventId),
                //     cancellationToken);
                //
                // var auditorium = availableAuditoriums.FirstOrDefault(x =>
                //     x.IsActive && teamsWithAuditoriums.Count(a => a.AuditoriumId == x.Key) < x.Capacity);
                //
                // if (auditorium == null)
                // {
                //     auditorium = availableAuditoriums.FirstOrDefault(x => x.IsDefaultAuditory);
                // }
                //
                // team.UpdateAuditorium(auditorium.Key);

                // TODO: Разобраться с project.MaxTeamCount
                // Сейчас по-умолчанию это значение равно 1, поэтому нет смысла в коде ниже. Значит, нужно его удалить
                //if (project.MaxTeamCount.HasValue) // && teamsWithAuditoriums.Count(x => x.ProjectId == project.Key) + 1 >= project.MaxTeamCount)
                //{
                // project.Disable();
                //}
                await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}