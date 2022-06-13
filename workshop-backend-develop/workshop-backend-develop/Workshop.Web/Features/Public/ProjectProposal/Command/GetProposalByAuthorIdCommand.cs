using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Public.ProjectProposal;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.ProjectProposal.Command
{
    public class GetProposalByAuthorIdCommand : IRequest<ProposalDto>
    {
    }

    public class GetProposalByAuthorIdCommandHandler : IRequestHandler<GetProposalByAuthorIdCommand,ProposalDto>
    {
        private readonly IUserProfileProvider _profileProvider;
        private readonly IProjectProposalRepository _proposalRepository;
        private readonly IMapper _mapper;
        public GetProposalByAuthorIdCommandHandler(IUserProfileProvider profileProvider,
                                            IProjectProposalRepository proposalRepository,IMapper mapper)
        {
            _profileProvider = profileProvider;
            _proposalRepository = proposalRepository;
            _mapper = mapper;
        }

        public  async Task<ProposalDto> Handle(GetProposalByAuthorIdCommand request, CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;
            var proposal = await _proposalRepository.FirstOrDefaultAsync(ProjectProposalSpecification.GetByAuthorId(user.EventId, user.Key), cancellationToken);
            if (proposal == null)
            {
                return null;
            }
            var dto = _mapper.Map<ProposalDto>(proposal);
            return dto;
        }
    }
}
