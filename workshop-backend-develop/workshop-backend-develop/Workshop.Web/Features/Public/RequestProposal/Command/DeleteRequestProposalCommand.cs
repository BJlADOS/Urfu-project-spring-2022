using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Public.RequestProposal
{
    public class DeleteRequestProposalCommand : IRequest
    {
        public long UserId { get; set; }
    }

    public class DeleteRequestProposalCommandHandler : AsyncRequestHandler<DeleteRequestProposalCommand>
    {
        private readonly RequestProposalRepository _requestProposalRepository;
        private readonly IUserProfileProvider _profile;
        public DeleteRequestProposalCommandHandler(RequestProposalRepository requestProposalRepository,
            IUserProfileProvider provider)
        {
            _profile = provider;
            _requestProposalRepository = requestProposalRepository;
        }

        protected override async Task Handle(DeleteRequestProposalCommand request, CancellationToken cancellationToken)
        {
            var author = _profile.GetProfile().User;
            var eventId = author.EventId;
            var proposal = await _requestProposalRepository
                .ListAsync(RequestProposalSpecification.GetByTeamLeadAndUserId(author.Key,request.UserId,eventId), cancellationToken);
            if(proposal is null || proposal.Length==0)
            {
                return;
            }
            await _requestProposalRepository.RemoveRangeAsync(proposal);
            await _requestProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}