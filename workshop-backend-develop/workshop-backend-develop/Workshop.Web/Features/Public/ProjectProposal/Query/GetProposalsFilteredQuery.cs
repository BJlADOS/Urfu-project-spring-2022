using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.ProjectProposal;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Features.Public.ProjectProposal.Query
{
    public class GetProposalsFilteredQuery : IRequest<ProposalDto[]>
    {
        public QueryProposalDto QueryProposalDto { get; set; }
    }

    public class
        GetProposalsFilteredQueryHandler : IRequestHandler<GetProposalsFilteredQuery, ProposalDto[]>
    {
        private readonly IMapper _mapper;
        private readonly ProjectProposalRepository _proposalRepository;
        private readonly IUserProfileProvider _profileProvider;

        public GetProposalsFilteredQueryHandler(IMapper mapper,
                                                ProjectProposalRepository proposalRepository,
                                                IUserProfileProvider profileProvider)
        {
            _mapper = mapper;
            _proposalRepository = proposalRepository;
            _profileProvider = profileProvider;
        }

        public async Task<ProposalDto[]> Handle(GetProposalsFilteredQuery request,
                                                CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;
            var proposals =
                await _proposalRepository.ListAsync(ProjectProposalSpecification
                                                               .GetByQuery(user.EventId,
                                                                request.QueryProposalDto.AuthorId,
                                                                request.QueryProposalDto.Status),
                                                           cancellationToken);
            var result = proposals
                         .Select(GetProposalDto)
                         .ToArray();
            return result;
        }

        private ProposalDto GetProposalDto(
            Core.Domain.Model.ProjectProposal.ProjectProposal proposal)
        {
            var result = _mapper.Map<ProposalDto>(proposal);
            result.Author = _mapper.Map<UsersListItemDto>(proposal.Author);
            return result;
        }
    }
}