using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Public.Project.Command
{
    public class ProjectDeleteCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class ProjectDeleteCommandHandler : AsyncRequestHandler<ProjectDeleteCommand>
    {
        private IUserProfileProvider _profileProvider;
        private ProjectRepository _projectRepository;
        private TeamRepository _teamRepository;

        public ProjectDeleteCommandHandler(ProjectRepository projectRepository, TeamRepository teamRepository, IUserProfileProvider profileProvider)
        {
            _profileProvider = profileProvider;
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
        }

        protected override async Task Handle(ProjectDeleteCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            var project = await _projectRepository.SingleAsync(ProjectSpecification.GetById(request.Id, eventId), cancellationToken);
            var teams = await _teamRepository.ListAsync(TeamSpecification.GetProjectTeams(request.Id, eventId), cancellationToken);

            foreach (var team in teams)
            {
                await _teamRepository.RemoveAsync(team);
            }

            await _projectRepository.RemoveAsync(project);
            await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
