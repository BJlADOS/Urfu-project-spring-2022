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
    public class DeleteProjectRoleCommand : IRequest
    {
        public long RoleId { get; set; }
    }

    public class DeleteProjectRoleCommandHandler : AsyncRequestHandler<DeleteProjectRoleCommand>
    {
        private readonly RoleRepository _rolesRepository;

        public DeleteProjectRoleCommandHandler(RoleRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        protected override async Task Handle(DeleteProjectRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _rolesRepository.SingleAsync(RoleSpecification.GetById(request.RoleId), cancellationToken);

            if (role == null)
            {
                return;
            }

            await _rolesRepository.RemoveAsync(role);
            await _rolesRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
