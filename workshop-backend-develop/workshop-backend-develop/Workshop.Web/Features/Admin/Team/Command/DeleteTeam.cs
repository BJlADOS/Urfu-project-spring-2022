using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;

namespace Workshop.Web.Features.Admin.Team.Command
{
    public class DeleteTeamCommand : IRequest
    {
        public long TeamId { get; set; }
    }

    public class DeleteTeamCommandHandler : AsyncRequestHandler<DeleteTeamCommand>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IUserProfileProvider _profileProvider;
        private readonly IProjectRepository _projectRepository;

        public DeleteTeamCommandHandler(ITeamRepository teamRepository, IUserProfileProvider profileProvider,
            IProjectRepository projectRepository)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
            _projectRepository = projectRepository;
        }

        protected override async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;
            var team = await _teamRepository.SingleOrDefaultAsync(TeamSpecification.GetById(request.TeamId, eventId),
                cancellationToken);

            if (team == null)
                throw new ArgumentException($"No team with id {request.TeamId} was found in event with id {eventId}");

            await _teamRepository.RemoveAsync(team);
            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}