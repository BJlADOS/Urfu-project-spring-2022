using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.ProjectProposal;

namespace Workshop.Web.Features.Public.ProjectProposal.Query
{
    public class ProposalsGetQuery : IRequest<ICollection<GetPendingProposals>>
    {
        public string Term { get; set; }
    }

    public class ProposalsGetQueryHandler : IRequestHandler<ProposalsGetQuery, ICollection<GetPendingProposals>>
    {
        private readonly ProjectProposalRepository _proposalRepository;
        private readonly IMapper _mapper;
        private IUserProfileProvider _profileProvider;
        public ProposalsGetQueryHandler(ProjectProposalRepository proposalRepository, IMapper mapper, IUserProfileProvider profileProvider)
        {
            _proposalRepository = proposalRepository;
            _mapper = mapper;
            _profileProvider = profileProvider;
        }

        public async Task<ICollection<GetPendingProposals>> Handle(ProposalsGetQuery request, CancellationToken cancellationToken)
        {
            
            var proposals = await _proposalRepository.ListAsync(ProjectProposalSpecification.GetPendingProposals(_profileProvider.GetProfile().User.EventId), cancellationToken);
            var result = proposals.Select(proposal => _mapper.Map<GetPendingProposals>(proposal)).ToList();
            return result;
        }
    }
}
