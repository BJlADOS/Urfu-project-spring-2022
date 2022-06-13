using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.Team.Command
{
    public class RenameTeamCommand : IRequest
    {
        public long TeamId { get; set; }
        public string Name { get; set; }
    }

    public class RenameTeamCommandHandler : AsyncRequestHandler<RenameTeamCommand>
    {
        private ITeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public RenameTeamCommandHandler(ITeamRepository teamRepository, IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(RenameTeamCommand request, CancellationToken cancellationToken)
        {
            var teamName = request.Name.Trim();
            if (teamName == "")
            {
                throw new ArgumentException();
            }
            var user = _profileProvider.GetProfile().User;

            var team = await _teamRepository.SingleOrDefaultAsync(
                TeamSpecification.GetById(request.TeamId, user.EventId),
                cancellationToken);

            if (team == null)
            {
                throw new NotFoundException();
            }

            if (team.TeamStatus != TeamStatus.Complete || (user.TeamId != request.TeamId && user.UserType != UserType.Admin))
            {
                throw new ForbiddenException();
            }

            team.UpdateName(teamName);

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
