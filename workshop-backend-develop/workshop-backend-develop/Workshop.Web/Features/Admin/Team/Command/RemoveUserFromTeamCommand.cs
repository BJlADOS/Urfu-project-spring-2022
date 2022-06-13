using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;

namespace Workshop.Web.Features.Admin.Team.Command
{
    public class RemoveUserFromTeamCommand : IRequest
    {
        public long UserId { get; set; }
    }

    public class RemoveUserFromTeamCommandHandler : AsyncRequestHandler<RemoveUserFromTeamCommand>
    {
        private IUserRepository _userRepository;
        private ITeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public RemoveUserFromTeamCommandHandler(IUserRepository userRepository, ITeamRepository teamRepository,
            IUserProfileProvider profileProvider)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(RemoveUserFromTeamCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleAsync(UserSpecification.GetById(request.UserId), cancellationToken);

            if (user.TeamId != null)
            {
                var team = await _teamRepository.SingleAsync(
                    TeamSpecification.GetById(user.TeamId.Value, _profileProvider.GetProfile().User.EventId),
                    cancellationToken);
                user.LeaveTeam();

                if (team.Users.Count <= 1)
                {
                    team.IncompleteTeam();
                }

                if (team.Users.Count == 1)
                {
                    team.UpdateName(null);
                }

                await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}