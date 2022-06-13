using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Public.ProjectProposal;

namespace Workshop.Web.Features.Public.ProjectProposal.Query
{
    public class GetProjectProposalById : IRequest<ProposalDto>
    {
        public long Id { get; set; }
    }

    public class GetProjectProposalByIdHandler : IRequestHandler<GetProjectProposalById, ProposalDto>
    {
        private readonly IUserProfileProvider _profileProvider;
        private readonly IMapper _mapper;
        private readonly IProjectProposalRepository _proposalRepository;

        public GetProjectProposalByIdHandler(IUserProfileProvider profileProvider,
                                             IMapper mapper,
                                             IProjectProposalRepository proposalRepository)
        {
            _profileProvider = profileProvider;
            _mapper = mapper;
            _proposalRepository = proposalRepository;
        }

        public async Task<ProposalDto> Handle(GetProjectProposalById request, 
                                                 CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;
            var proposal = await _proposalRepository.SingleOrDefaultAsync(
             ProjectProposalSpecification.GetById(request.Id, user.EventId), cancellationToken);
            var dto = _mapper.Map<ProposalDto>(proposal);
            return dto;
        }
    }
}