using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Admin.Projects.Command
{
    public class OpenCloseAllProjectsCommand : IRequest
    {
        public bool IsAvailable { get; set; }
    }

    public class OpenCloseAllProjectsCommandHandler : AsyncRequestHandler<OpenCloseAllProjectsCommand>
    {
        private readonly ProjectRepository _projectsRepository;
        private IUserProfileProvider _profileProvider;


        public OpenCloseAllProjectsCommandHandler(ProjectRepository repository, IUserProfileProvider profileProvider)
        {
            _projectsRepository = repository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(OpenCloseAllProjectsCommand request, CancellationToken cancellationToken)
        {
            var projects = await _projectsRepository.ListAsync(
                ProjectSpecification.GetAll(_profileProvider.GetProfile().User.EventId),
                cancellationToken
                );

            foreach (var project in projects)
            {
                project.UpdateAvailability(request.IsAvailable);
            }

            await _projectsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
