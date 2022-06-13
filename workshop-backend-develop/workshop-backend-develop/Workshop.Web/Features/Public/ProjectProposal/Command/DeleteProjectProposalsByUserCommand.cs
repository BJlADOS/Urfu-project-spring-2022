using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Public.ProjectProposal.Command
{
    public class DeleteProjectProposalsByUserCommand : IRequest
    {
    }
    public class DeleteProjectProposalsByUserCommandHandler : AsyncRequestHandler<DeleteProjectProposalsByUserCommand>
    {
        private readonly IUserProfileProvider _profileProvider;
        private readonly ProjectProposalRepository _projectProposalRepository;

        public DeleteProjectProposalsByUserCommandHandler(IUserProfileProvider profileProvider,
                                                   ProjectProposalRepository
                                                       projectProposalRepository)
        {
            _profileProvider = profileProvider;
            _projectProposalRepository = projectProposalRepository;
        }

        protected async override Task Handle(DeleteProjectProposalsByUserCommand request, CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;
            var proposals = await _projectProposalRepository.ListAsync(ProjectProposalSpecification.GetById(user.Key, user.EventId), cancellationToken);
            if (proposals is null)
            {
                return;
            }

            await _projectProposalRepository.RemoveRangeAsync(proposals);
            await _projectProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
