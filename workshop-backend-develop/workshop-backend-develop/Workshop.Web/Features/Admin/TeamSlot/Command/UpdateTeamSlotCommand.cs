using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.TeamSlot;
using Workshop.Core.Domain.Specification;
using Workshop.Web.Dtos.Admin.TeamSlot;

namespace Workshop.Web.Features.Admin.TeamSlot.Command
{
    public class UpdateTeamSlotCommand : IRequest
    {
        public UpdateTeamSlotDto Dto { get; set; }
    }
    
    public class UpdateTeamSlotCommandHandler: AsyncRequestHandler<UpdateTeamSlotCommand>
    {
        private readonly ITeamSlotsRepository _teamSlotsRepository;

        public UpdateTeamSlotCommandHandler(ITeamSlotsRepository teamSlotsRepository)
        {
            _teamSlotsRepository = teamSlotsRepository;
        }

        protected override async Task Handle(UpdateTeamSlotCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;
            var teamSlot =
                await _teamSlotsRepository.SingleAsync(TeamSlotSpecification.GetById(dto.Id),
                                                       cancellationToken);
            if (teamSlot is null)
            {
                return;
            }

            teamSlot.AuditoriumId = dto.AuditorumId;
            teamSlot.TeamId = dto.TeamId;
            teamSlot.Time = dto.Time;
            await _teamSlotsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}