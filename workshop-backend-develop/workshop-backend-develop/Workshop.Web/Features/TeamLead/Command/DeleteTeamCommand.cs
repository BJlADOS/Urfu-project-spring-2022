using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.TeamLead.Command
{
    public class DeleteTeamCommand : IRequest
    {
        public User User { get; set; }
    }

    public class DeleteTeamCommandHandler : AsyncRequestHandler<DeleteTeamCommand>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly RequestProposalRepository _requestProposalRepository;
        private readonly RoleRepository _roleRepository;
        private readonly UserRepository _userRepository;

        public DeleteTeamCommandHandler(ITeamRepository teamRepository,
            RequestProposalRepository requests,
            RoleRepository roles,
            UserRepository users)
        {
            _teamRepository = teamRepository;
            _requestProposalRepository = requests;
            _roleRepository = roles;
            _userRepository = users;
        }

        protected override async Task Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var eventId = request.User.EventId;
            var team = await _teamRepository.SingleOrDefaultAsync(TeamSpecification.GetById(request.User.Team.Key, eventId),
                cancellationToken);

            if (team == null)
                throw new ArgumentException("No team with id " + request.User.Key + " was found in event with id " +
                                            eventId);

            var users = await _userRepository.ShallowListAsyncWithRoles(
                UserSpecification.GetByIds(team.Users.Select(x => x.Key).ToHashSet()), cancellationToken);

            foreach (var user in users) 
                user.LeaveTeam();

            var proposals = await _requestProposalRepository.ListAsync(
                RequestProposalSpecification.GetByTeamleadId(request.User.Key, request.User.EventId),
                cancellationToken);

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _roleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _requestProposalRepository.RemoveRangeAsync(proposals);
            await _requestProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _teamRepository.RemoveAsync(team);
            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}