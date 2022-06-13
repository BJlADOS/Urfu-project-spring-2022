using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.TeamLead.Command
{
    public class AddUserInTeamCommand : IRequest
    {
        public long UserId { get; set; }
        public long TeamId { get; set; }
        public string RoleName { get; set; }
    }

    public class AddUserInTeamCommandHandler : AsyncRequestHandler<AddUserInTeamCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly RequestProposalRepository _proposalRepository;
        private readonly IUserProfileProvider _profileProvider;

        public AddUserInTeamCommandHandler(IUserRepository users, ITeamRepository teams,
            IProjectRepository projects,
            IUserProfileProvider provider,
            RequestProposalRepository requests)
        {
            _userRepository = users;
            _teamRepository = teams;
            _projectRepository = projects;
            _profileProvider = provider;
            _proposalRepository = requests;
        }

        protected override async Task Handle(AddUserInTeamCommand request, CancellationToken cancellationToken)
        {
            var teamlead =
                await _userRepository.SingleAsync(UserSpecification.GetById(_profileProvider.GetProfile().User.Key),
                    cancellationToken);

            var userToAdd =
                await _userRepository.SingleOrDefaultAsync(UserSpecification.GetById(request.UserId),
                    cancellationToken);

            if (userToAdd == null || userToAdd.TeamId.HasValue)
            {
                throw new ForbiddenException();
            }

            var usersInTeamleadTeamCount =
                await _userRepository.CountAsync(
                    UserSpecification.GetAllStudentsInTeam(teamlead.EventId, request.TeamId),
                    cancellationToken);

            var project = await _projectRepository.SingleAsync(
                ProjectSpecification.GetById(teamlead.Team.ProjectId, teamlead.EventId),
                cancellationToken);

            var proposal = await _proposalRepository.ListAsync(
                RequestProposalSpecification.GetByUserId(request.UserId, teamlead.EventId), cancellationToken);

            if (proposal != null)
            {
                await _proposalRepository.RemoveRangeAsync(proposal);
                await _proposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            if (usersInTeamleadTeamCount + 1 > project.TeamCapacity)
                throw new ForbiddenException();

            var currentRole =
                project.Roles
                    .ToNoneDuplicatedDictionary(key => key.Name, value => value.Key);

            if (currentRole.TryGetValue(request.RoleName, out var result))
                userToAdd.UpdateRole(result);

            userToAdd.UpdateTeam(teamlead.Team.Key);


            if (usersInTeamleadTeamCount + 1 >= project.TeamCapacity || (usersInTeamleadTeamCount + 1 >= 3 && project.TeamCapacity >= 4))
            {
                if (teamlead.Team.TeamStatus == TeamStatus.Incomplete)
                {
                    teamlead.Team.GetReadyForEntry();
                    await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
                }
            }

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}