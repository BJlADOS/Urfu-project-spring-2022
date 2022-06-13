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
    public class RejectProjectProposalCommand : IRequest
    {
        public long ProposalId { get; set; } 
    }
    public class RejectProjectProposalCommandHandler : AsyncRequestHandler<RejectProjectProposalCommand>
    {
        private readonly IUserProfileProvider _profileProvider;
        private readonly ProjectProposalRepository _projectProposalRepository;

        public RejectProjectProposalCommandHandler(IUserProfileProvider profile, ProjectProposalRepository proposals)
        {
            _profileProvider = profile;
            _projectProposalRepository = proposals;
        }

        protected override async Task Handle(RejectProjectProposalCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            var proposal = await _projectProposalRepository.FirstOrDefaultAsync(ProjectProposalSpecification.GetById(request.ProposalId, eventId), cancellationToken);
            if (proposal is null)
            {
                return;
            }
            proposal.UpdateStatus(Core.Domain.Model.ProjectProposal.ProposalStatus.Rejected);

            await _projectProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
