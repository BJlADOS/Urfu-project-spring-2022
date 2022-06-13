using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.Project.Command
{
    public class LeaveTeamCommand : IRequest
    {
        public long UserId { get; set; }
    }

    public class LeaveTeamCommandHandler : AsyncRequestHandler<LeaveTeamCommand>
    {
        private IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;
        private IUserRepository _userRepository;
        private ITeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public LeaveTeamCommandHandler(ITeamRepository teamRepository, IUserRepository userRepository,
            IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository,
            IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(LeaveTeamCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleAsync(UserSpecification.GetById(request.UserId), cancellationToken);

            if (!user.TeamId.HasValue)
            {
                throw new NotFoundException();
            }

            var team = await _teamRepository.SingleOrDefaultAsync(
                TeamSpecification.GetById(user.TeamId.Value, _profileProvider.GetProfile().User.EventId),
                cancellationToken);

            if (team == null)
            {
                throw new NotFoundException();
            }

      

            user.LeaveTeam();
            if (team.Users.Count <= 3)
            {
                team.IncompleteTeam();
            }
            
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}