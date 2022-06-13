using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;

namespace Workshop.Web.Features.Admin.Team.Command
{
    public class UpdateTeamProject : IRequest
    {
        public long ProjectId { get; set; }

        public long TeamId { get; set; }
    }

    public class UpdateTeamProjectHandler : AsyncRequestHandler<UpdateTeamProject>
    {
        private ITeamRepository _teamRepository;
        private IProjectRepository _projectRepository;
        private IUserProfileProvider _profileProvider;

        public UpdateTeamProjectHandler(ITeamRepository teams, IProjectRepository projects,
            IUserProfileProvider profile)
        {
            _teamRepository = teams;
            _profileProvider = profile;
            _projectRepository = projects;
        }

        protected override async Task Handle(UpdateTeamProject request, CancellationToken cancellationToken)
        {

            var eventId = _profileProvider.GetProfile().User.EventId;
            var project = await _projectRepository.SingleOrDefaultAsync(
                ProjectSpecification.GetById(request.ProjectId, eventId ),
                cancellationToken);

            if (project is null)
            {
                throw new ArgumentException();
            }

            var team = await _teamRepository.SingleAsync(TeamSpecification.GetById(request.TeamId, eventId),
                cancellationToken);

            team.UpdateProject(project);

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
