using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Role;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Admin.Projects.Command
{
    public class AddProjectRoleCommand : IRequest
    {
        public string RoleName { get; set; }
        public long ProjectId { get; set; }
    }

    public class AddProjectRoleCommandHandler : AsyncRequestHandler<AddProjectRoleCommand>
    {
        private readonly RoleRepository _rolesRepository;
        private readonly ProjectRepository _projectsRepository;
        private IUserProfileProvider _profileProvider;


        public AddProjectRoleCommandHandler(RoleRepository rolesRepository, ProjectRepository projectsRepository, IUserProfileProvider profileProvider)
        {
            _rolesRepository = rolesRepository;
            _projectsRepository = projectsRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(AddProjectRoleCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectsRepository.SingleAsync(
                ProjectSpecification.GetById(request.ProjectId, _profileProvider.GetProfile().User.EventId),
                cancellationToken
                );

            if (project == null)
            {
                return;
            }

            var role = new Role(request.RoleName, project);

            await _rolesRepository.AddAsync(role, cancellationToken);
            await _rolesRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
