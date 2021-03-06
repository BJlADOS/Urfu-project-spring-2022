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
    public class FinishTeamCommand : IRequest
    {
        public long TeamId { get; set; }
        public int Mark { get; set; }
        public string Comment { get; set; }
    }

    public class FinishTeamCommandHandler : AsyncRequestHandler<FinishTeamCommand>
    {
        private TeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public FinishTeamCommandHandler(TeamRepository teamRepository, IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(FinishTeamCommand request, CancellationToken cancellationToken)
        {
            var team = await _teamRepository.SingleAsync(TeamSpecification.GetById(request.TeamId, _profileProvider.GetProfile().User.EventId), cancellationToken);
            team.Finish();
            team.UpdateComment(request.Comment);
            team.UpdateMark(request.Mark);
            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
