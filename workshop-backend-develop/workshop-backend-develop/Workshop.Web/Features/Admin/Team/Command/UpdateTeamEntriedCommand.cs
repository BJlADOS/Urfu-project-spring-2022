using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Admin.Team.Command
{
    public class UpdateTeamEntriedCommand : IRequest
    {
        public long TeamId { get; set; }
    }
    public class UpdateTeamEntriedCommandHandler : AsyncRequestHandler<UpdateTeamEntriedCommand>
    {
        private readonly TeamRepository _teamRepository;
        private readonly IUserProfileProvider _profileProvider;
        public UpdateTeamEntriedCommandHandler(TeamRepository teams, IUserProfileProvider provider)
        {
            _teamRepository = teams;
            _profileProvider = provider;
        }

        protected override async Task Handle(UpdateTeamEntriedCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            var team = await _teamRepository.SingleOrDefaultAsync(TeamSpecification.GetById(request.TeamId, eventId), cancellationToken);
            if (team is null)
            {
                throw new NotFoundException();
            }
            team.UpdateEntried(true);
            team.CompleteTest();

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
