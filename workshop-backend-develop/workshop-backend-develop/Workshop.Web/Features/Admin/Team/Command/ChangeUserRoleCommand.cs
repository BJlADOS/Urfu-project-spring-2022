using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;

namespace Workshop.Web.Features.Admin.Team.Command
{
    public class ChangeUserRoleCommand : IRequest
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }
    }

    public class ChangeUserRoleCommandHandler : AsyncRequestHandler<ChangeUserRoleCommand>
    {
        private readonly IUserRepository _userRepository;


        public ChangeUserRoleCommandHandler(IUserRepository repository)
        {
            _userRepository = repository;
        }

        protected override async Task Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleAsync(UserSpecification.GetById(request.UserId),
                cancellationToken);

            user.UpdateRole(request.RoleId);

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
