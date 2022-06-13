using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Admin.Project.Command
{
    public class CloseProjectsForEntryCommand : IRequest
    {
        public bool IsOpen { get; set; }
    }

    public class CloseProjectsForEnrtryCommandHelper : AsyncRequestHandler<OpenProjectsForEnrtryCommand>
    {
        private readonly ProjectRepository _projectsRepository;
        private IUserProfileProvider _profileProvider;

        public CloseProjectsForEnrtryCommandHelper(ProjectRepository repository, IUserProfileProvider profile)
        {
            _projectsRepository = repository;
            _profileProvider = profile;
        }

        protected override async Task Handle(OpenProjectsForEnrtryCommand request, CancellationToken cancellationToken)
        {
            var projects = await _projectsRepository.ListAsync(
                ProjectSpecification.GetAll(_profileProvider.GetProfile().User.EventId),
                cancellationToken
                );
            foreach (var project in projects.Where(p => p.IsAvailable))
            {
                project.UpdateAvailability(!request.IsOpen);
                project.UpdateEntryOpen(request.IsOpen);
            }
            await _projectsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
