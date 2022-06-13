using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.TeamLead.Command
{
    public class UpdateProjectsForEntryCommand : IRequest
    {
        public long TeamId { get; set; }
    }

    public class UpdateProjectsForEntryCommandHandler : AsyncRequestHandler<UpdateProjectsForEntryCommand>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUserProfileProvider _profile;
        private readonly IProjectRepository _projectRepository;

        public UpdateProjectsForEntryCommandHandler(ITeamRepository teamRepository, IUserProfileProvider provider,
            IProjectRepository projectRepository)
        {
            _teamRepository = teamRepository;
            _profile = provider;
            _projectRepository = projectRepository;
        }

        protected override async Task Handle(UpdateProjectsForEntryCommand request, CancellationToken cancellationToken)
        {
            var user = _profile.GetProfile().User;
            var team = await _teamRepository.FirstAsync(TeamSpecification.GetById(request.TeamId, user.EventId),
                cancellationToken);
            if (team is null)
            {
                throw new NotFoundException();
            }

            if (team.TeamStatus == TeamStatus.ReadyForEntry)
            {
                var project =
                    await _projectRepository.SingleAsync(ProjectSpecification.GetById(team.ProjectId, user.EventId),
                        cancellationToken);

                if (!project.IsEntryOpen)
                {
                    throw new ForbiddenException();
                }

                var teamsEntried = await _teamRepository.LongCountAsync(
                    TeamSpecification.GetEntriedByProjectId(project.Key, user.EventId), cancellationToken);
                //var allTeams = await _teamRepository.ListAsync(TeamSpecification.GetEntriedByProjectId(request.))
                if (teamsEntried >= project.MaxTeamCount)
                {
                    throw new ForbiddenException();
                }

                team.UpdateEntried(true);
                team.CompleteTest();

                if (teamsEntried + 1 >= project.MaxTeamCount)
                {
                    project.UpdateEntryOpen(false);
                    project.UpdateAvailability(false);
                }

                await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}