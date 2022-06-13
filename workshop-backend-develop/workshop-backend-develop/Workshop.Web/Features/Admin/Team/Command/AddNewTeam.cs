using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;

namespace Workshop.Web.Features.Admin.Team.Command
{
    public class AddNewTeamCommand : IRequest
    {
        public long ProjectId { get; set; }
    }

    public class AddNewTeamCommandHandler : AsyncRequestHandler<AddNewTeamCommand>
    {
        private ITeamRepository _teamRepository;
        private IProjectRepository _projectRepository;
        private IUserProfileProvider _profileProvider;

        public AddNewTeamCommandHandler(ITeamRepository teamRepository, IProjectRepository projectRepository,
            IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
            _projectRepository = projectRepository;
        }

        protected override async Task Handle(AddNewTeamCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            var project = await _projectRepository.SingleAsync(
                ProjectSpecification.GetById(request.ProjectId, eventId),
                cancellationToken);

            var newTeam = new Workshop.Core.Domain.Model.Team.Team(project, eventId);

            newTeam.UpdateEntried(true);
            newTeam.CompleteTest();
            await _teamRepository.AddAsync(newTeam, cancellationToken);

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}