using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.TeamSlot;
using Workshop.Core.Domain.Specification;

namespace Workshop.Web.Features.Admin.TeamSlot.Command
{
    public class DeleteTeamSlotCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteTeamSlotCommandHandler : AsyncRequestHandler<DeleteTeamSlotCommand>
    {
        private readonly ITeamSlotsRepository _teamSlotsRepository;

        public DeleteTeamSlotCommandHandler(ITeamSlotsRepository teamSlotsRepository)
        {
            _teamSlotsRepository = teamSlotsRepository;
        }

        protected override async Task Handle(DeleteTeamSlotCommand request, CancellationToken cancellationToken)
        {
            var slot = await _teamSlotsRepository.SingleAsync(TeamSlotSpecification.GetById(request.Id),
                cancellationToken);
            slot.Team?.UpdateTeamSlot(null);
            await _teamSlotsRepository.RemoveAsync(slot);
            await _teamSlotsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}