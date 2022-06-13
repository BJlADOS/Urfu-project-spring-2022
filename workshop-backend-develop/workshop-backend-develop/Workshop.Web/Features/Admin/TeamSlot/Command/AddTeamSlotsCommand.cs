using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.TeamSlot;
using Workshop.Web.Dtos.Admin.TeamSlot;

namespace Workshop.Web.Features.Admin.TeamSlot.Command
{
    public class AddTeamSlotsCommand : IRequest
    {
        public AddTeamSlotsDto Dto { get; set; }
    }

    public class AddTeamSlotsCommandHandler : AsyncRequestHandler<AddTeamSlotsCommand>
    {
        private readonly ITeamSlotsRepository _teamSlotsRepository;

        public AddTeamSlotsCommandHandler(ITeamSlotsRepository teamSlotsRepository)
        {
            _teamSlotsRepository = teamSlotsRepository;
        }
        
        protected override async Task Handle(AddTeamSlotsCommand request, CancellationToken cancellationToken)
        {
            var slots = new List<Core.Domain.Model.TeamSlot.TeamSlot>();
            var startTime = request.Dto.StartTime;
            var endTime = request.Dto.EndTime;
            var duration = TimeSpan.FromMinutes(request.Dto.Duration);
            for (var time = startTime; time < endTime; time = time.Add(duration))
                slots.Add(new Core.Domain.Model.TeamSlot.TeamSlot(request.Dto.AuditoriumId, time));
            await _teamSlotsRepository.AddRangeAsync(slots, cancellationToken);
            await _teamSlotsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}