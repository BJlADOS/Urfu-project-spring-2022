using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Expert.Team.Command
{
    public class StartTestTeamCommand : IRequest
    {
        public long TeamId { get; set; }
    }

    public class StartTestTeamCommandHandler : AsyncRequestHandler<StartTestTeamCommand>
    {
        private ITeamRepository _teamRepository;
        private IReadOnlyRepository<Core.Domain.Model.Event.Event> _eventRepository;
        private IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;
        private IReadOnlyRepository<Core.Domain.Model.Auditorium.Auditorium> _auditoriumRepository;
        private IUserProfileProvider _profileProvider;

        public StartTestTeamCommandHandler(ITeamRepository teamRepository, IUserProfileProvider profileProvider,
            IReadOnlyRepository<Core.Domain.Model.Event.Event> eventRepository,
            IReadOnlyRepository<Core.Domain.Model.Auditorium.Auditorium> auditoriumRepository,
            IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
            _eventRepository = eventRepository;
            _auditoriumRepository = auditoriumRepository;
            _projectRepository = projectRepository;
        }

        protected override async Task Handle(StartTestTeamCommand request, CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;
            var team = await _teamRepository.SingleAsync(
                TeamSpecification.GetById(request.TeamId, user.EventId),
                cancellationToken);
            var project = await _projectRepository.SingleAsync(ProjectSpecification.GetById(team.ProjectId, user.EventId), cancellationToken);

            if ((user.UserType == Core.Domain.Model.User.UserType.Student || user.UserType == Core.Domain.Model.User.UserType.Teamlead) && !team.Users.Select(u => u.Key).Contains(user.Key))
            {
                throw new ForbiddenException();
            }

            var availableAuditoriums = await _auditoriumRepository.ListAsync(
                AuditoriumSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);

            var teamsWithAuditoriums = await _teamRepository.ListAsync(
                TeamSpecification.GetTeamsWithAuditorium(_profileProvider.GetProfile().User.EventId),
                cancellationToken);

            team.CompleteTest();
            team.UpdateTeamCompleteDate(DateTimeHelper.GetCurrentTime());

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}