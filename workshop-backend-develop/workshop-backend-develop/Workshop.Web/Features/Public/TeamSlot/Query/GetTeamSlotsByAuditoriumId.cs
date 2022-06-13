using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.TeamSlot;
using Workshop.Core.Domain.Specification;
using Workshop.Web.Dtos.Public.TeamSlot;

namespace Workshop.Web.Features.Public.TeamSlot.Query
{
    public class GetTeamSlotsByAuditoriumId : IRequest<TeamSlotDto[]>
    {
        public long Id { get; set; }
    }

    public class GetTeamSlotsByAuditoriumIdHandler : IRequestHandler<GetTeamSlotsByAuditoriumId, TeamSlotDto[]>
    {
        private readonly IMapper _mapper;
        private readonly ITeamSlotsRepository _slotsRepository;
        
        public GetTeamSlotsByAuditoriumIdHandler(ITeamSlotsRepository slotsRepository, IMapper mapper)
        {
            _slotsRepository = slotsRepository;
            _mapper = mapper;
        }

        public async Task<TeamSlotDto[]> Handle(
            GetTeamSlotsByAuditoriumId request, CancellationToken cancellationToken) 
            => (await _slotsRepository.ListAsync(TeamSlotSpecification.GetByAuditoriumId(request.Id), cancellationToken))
                .Select(x => _mapper.Map<TeamSlotDto>(x)).ToArray();
    }
}