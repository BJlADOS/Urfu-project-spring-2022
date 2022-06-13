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
    public class EditProjectRoleCommand : IRequest
    {
        public string RoleName { get; set; }
        public long RoleId { get; set; }
    }

    public class EditProjectRoleCommandHandler : AsyncRequestHandler<EditProjectRoleCommand>
    {
        private readonly RoleRepository _rolesRepository;

        public EditProjectRoleCommandHandler(RoleRepository rolesRepository)
        {
            _rolesRepository = rolesRepository;
        }

        protected override async Task Handle(EditProjectRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _rolesRepository.SingleAsync(RoleSpecification.GetById(request.RoleId), cancellationToken);

            if (role == null)
            {
                return;
            }

            role.UpdateName(request.RoleName);

            await _rolesRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
