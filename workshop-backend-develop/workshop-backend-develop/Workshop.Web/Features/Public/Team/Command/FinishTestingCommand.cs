using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.Project.Command
{
    public class FinishTestingCommand : IRequest
    {
        
    }
    
    public class FinishTestingCommandHandler : AsyncRequestHandler<FinishTestingCommand>
    {
        private ITeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public FinishTestingCommandHandler(IReadOnlyRepository<Core.Domain.Model.User.User> userRepository, ITeamRepository teamRepository, IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(FinishTestingCommand request, CancellationToken cancellationToken)
        {
            var teamId = _profileProvider.GetProfile().User.TeamId;
            
            if (!teamId.HasValue)
                throw new ForbiddenException();
            
            var team = await _teamRepository.SingleAsync(TeamSpecification.GetById(teamId.Value, _profileProvider.GetProfile().User.EventId), cancellationToken);

            if (team.TeamStatus != TeamStatus.TestWork)
            {
                throw new ForbiddenException();
            }
            
            team.CompleteTest();
            team.UpdateTeamCompleteDate(DateTimeHelper.GetCurrentTime());

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}