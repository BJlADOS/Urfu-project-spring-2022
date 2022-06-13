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
    public class OpenTeamCommand : IRequest
    {
        public long TeamId { get; set; }
    }

    public class OpenTeamCommandHandler : AsyncRequestHandler<OpenTeamCommand>
    {
        private ITeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public OpenTeamCommandHandler(ITeamRepository teamRepository, IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(OpenTeamCommand request, CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;

            var team = await _teamRepository.SingleOrDefaultAsync(
                TeamSpecification.GetById(request.TeamId, user.EventId),
                cancellationToken);

            if (team == null)
            {
                throw new NotFoundException();
            }

            if ((user.TeamId != request.TeamId || !team.Project.IsAvailable) && user.UserType != UserType.Admin)
            {
                throw new ForbiddenException();
            }

            team.IncompleteTeam();

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
