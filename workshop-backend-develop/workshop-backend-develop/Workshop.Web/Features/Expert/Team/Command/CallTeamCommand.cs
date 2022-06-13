using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;


namespace Workshop.Web.Features.Expert.Team.Command
{
    public class CallTeamCommand : IRequest
    {
        public long TeamId { get; set; }
        public long AuditoriumId { get; set; }
    }

    public class CallTeamCommandHandler : AsyncRequestHandler<CallTeamCommand>
    {
        private TeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public CallTeamCommandHandler(TeamRepository teamRepository, IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(CallTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.SingleAsync(TeamSpecification.GetById(request.TeamId, _profileProvider.GetProfile().User.EventId), cancellationToken);
            //team.UpdateExpert(request.ExpertId);
            //team.ExpertDiscussion();
            team.UpdateAuditorium(request.AuditoriumId);
            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
