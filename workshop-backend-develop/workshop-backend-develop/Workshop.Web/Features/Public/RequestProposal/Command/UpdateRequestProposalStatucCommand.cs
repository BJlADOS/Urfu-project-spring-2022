using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.RequestProposal;

namespace Workshop.Web.Features.Public.RequestProposal
{
    public class UpdateRequestProposalStatucCommand : IRequest
    {
        public UpdateStatusRequestProposalDto UpdateStatusDto { get; set; }
        public long Id { get; set; }

    }

    public class UpdateRequestProposalStatucCommandHandler : AsyncRequestHandler<UpdateRequestProposalStatucCommand>
    {
        private readonly RequestProposalRepository _requestProposalRepository;
        private readonly IUserProfileProvider _provider;
        public UpdateRequestProposalStatucCommandHandler(RequestProposalRepository requestProposalRepository,
            IUserProfileProvider provider)
        {
            _provider = provider;
            _requestProposalRepository = requestProposalRepository;
        }

        protected override async Task Handle(UpdateRequestProposalStatucCommand request,
            CancellationToken cancellationToken)
        {
            var eventId = _provider.GetProfile().User.EventId;
            var requestProposal = await _requestProposalRepository.SingleAsync(
                RequestProposalSpecification.GetById(request.Id,eventId),
                cancellationToken);

            requestProposal.Status = request.UpdateStatusDto.Status;
            await _requestProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}