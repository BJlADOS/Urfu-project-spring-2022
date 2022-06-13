using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.RequestProposal;

namespace Workshop.Web.Features.Public.RequestProposal.Query
{
    public class GetAllRequestProposalByTeamleadId : IRequest<RequestProposalDto[]>
    {
        public long TeamleadId { get; set; }
    }

    public class GetAllRequestProposalByTeamleadIdHandler
        : IRequestHandler<GetAllRequestProposalByTeamleadId, RequestProposalDto[]>
    {
        private readonly RequestProposalRepository _requestProposalRepository;
        private readonly IMapper _mapper;
        private readonly IUserProfileProvider _provider;

        public GetAllRequestProposalByTeamleadIdHandler(RequestProposalRepository requestProposalRepository,
            IMapper mapper,
            IUserProfileProvider provider)
        {
            _requestProposalRepository = requestProposalRepository;
            _mapper = mapper;
            _provider = provider;
        }

        public async Task<RequestProposalDto[]> Handle(GetAllRequestProposalByTeamleadId request,
            CancellationToken cancellationToken)
        {
            var eventId = _provider.GetProfile().User.EventId;
            var proposals = await _requestProposalRepository
                .ListAsync(RequestProposalSpecification.GetByTeamleadId(request.TeamleadId,eventId), cancellationToken);

            return proposals.Select(p => _mapper.Map<RequestProposalDto>(p)).ToArray();
        }
    }
}