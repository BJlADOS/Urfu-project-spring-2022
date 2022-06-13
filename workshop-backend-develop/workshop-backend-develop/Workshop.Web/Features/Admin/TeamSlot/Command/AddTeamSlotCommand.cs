using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.TeamSlot;
using Workshop.Web.Dtos.Admin.TeamSlot;

namespace Workshop.Web.Features.Admin.TeamSlot.Command
{
    public class AddTeamSlotCommand : IRequest
    {
        public AddTeamSlotDto Dto { get; set; }
    }

    public class AddTeamSlotCommandHandler : AsyncRequestHandler<AddTeamSlotCommand>
    {
        private readonly ITeamSlotsRepository _teamSlotsRepository;

        public AddTeamSlotCommandHandler(ITeamSlotsRepository teamSlotsRepository)
        {
            _teamSlotsRepository = teamSlotsRepository;
        }

        protected override async Task Handle(AddTeamSlotCommand request,
                                             CancellationToken cancellationToken)
        {
            var slot =
                new Core.Domain.Model.TeamSlot.TeamSlot(request.Dto.AuditoriumId, request.Dto.Time);
            await _teamSlotsRepository.AddAsync(slot, cancellationToken);
            await _teamSlotsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}