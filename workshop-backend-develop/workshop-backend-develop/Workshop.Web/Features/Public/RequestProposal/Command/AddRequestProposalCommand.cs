using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.RequestProposal;

namespace Workshop.Web.Features.Public.RequestProposal
{
    public class AddRequestProposalCommand : IRequest
    {
        public AddRequestProposalDto AddRequestProposalDto { get; set; }
    }

    public class AddRequestProposalCommandHandler : AsyncRequestHandler<AddRequestProposalCommand>
    {
        private readonly RequestProposalRepository _requestProposalRepository;
        private readonly IUserProfileProvider _profileProvider;

        public AddRequestProposalCommandHandler(RequestProposalRepository requestProposalRepository, IUserProfileProvider provider)
        {
            _requestProposalRepository = requestProposalRepository;
            _profileProvider = provider;
        }

        protected override async Task Handle(AddRequestProposalCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            var proposal = new Core.Domain.Model.RequestProposal.RequestProposal(request.AddRequestProposalDto, eventId);

            if (await _requestProposalRepository.LongCountAsync(RequestProposalSpecification.GetByTeamLeadAndUserId(request.AddRequestProposalDto.TeamleadId, request.AddRequestProposalDto.UserId, eventId), cancellationToken) == 0)
            {

                await _requestProposalRepository.AddAsync(proposal, cancellationToken);
                await _requestProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}