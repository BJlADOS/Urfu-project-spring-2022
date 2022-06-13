using System;
using System.Threading;
using System.Threading.Tasks;
using Aspose.Cells;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.TeamSlot;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Public.TeamSlot;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.TeamSlot.Command
{
    public class SignTeamInTeamSlotCommand : IRequest
    {
        public ShortTeamSlotDto Dto { get; set; }
    }

    public class SignTeamInTeamSlotCommandHandler : AsyncRequestHandler<SignTeamInTeamSlotCommand>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamSlotsRepository _teamSlotsRepository;
        private readonly IUserProfileProvider _profileProvider;

        public SignTeamInTeamSlotCommandHandler(ITeamRepository teamRepository,
                                                ITeamSlotsRepository teamSlotsRepository,
                                                IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _teamSlotsRepository = teamSlotsRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(SignTeamInTeamSlotCommand request,
                                             CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            var dto = request.Dto;
            var team =
                await _teamRepository
                    .SingleOrDefaultAsync(TeamSpecification.GetById(dto.TeamId, eventId),
                                          cancellationToken);
            if (team is null)
            {
                return;
            }

            var teamSlot = await _teamSlotsRepository.SingleOrDefaultAsync(TeamSlotSpecification.GetById(dto.SlotId), cancellationToken);

            if (teamSlot is null)
            {
                throw new ArgumentException("Team Slot with passed id not exist.");
            }

            if (teamSlot.TeamId != null)
            {
                throw new ForbiddenException();
            }

            teamSlot.SignTeam(team);
            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _teamSlotsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}