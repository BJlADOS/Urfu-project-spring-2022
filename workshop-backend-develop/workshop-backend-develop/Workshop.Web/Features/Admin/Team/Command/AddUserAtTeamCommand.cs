using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Admin.Team.Command
{
    public class AddUserAtTeamCommand : IRequest
    {
        public long UserId { get; set; }
        public long TeamId { get; set; }
        public long RoleId { get; set; }
    }

    public class AddUserAtTeamCommandHandler : AsyncRequestHandler<AddUserAtTeamCommand>
    {
        private readonly UserRepository _userRepository;
        private readonly TeamRepository _teamRepository;
        private readonly IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;

        private readonly IUserProfileProvider _profileProvider;

        public AddUserAtTeamCommandHandler(UserRepository userRepository, TeamRepository teamRepository,
            IUserProfileProvider profileProvider,
            IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository)
        {
            _userRepository = userRepository;
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
            _projectRepository = projectRepository;
        }

        protected override async Task Handle(AddUserAtTeamCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.SingleAsync(UserSpecification.GetById(request.UserId), cancellationToken);

            var team = await _teamRepository.SingleAsync(
                TeamSpecification.GetById(request.TeamId, _profileProvider.GetProfile().User.EventId),
                cancellationToken);

            var project = await _projectRepository.SingleAsync(
                ProjectSpecification.GetById(team.ProjectId, _profileProvider.GetProfile().User.EventId),
                cancellationToken);

            user.UpdateTeam(request.TeamId);
            user.UpdateRole(request.RoleId);

           

            if (team.Users.Count + 1 >= project.TeamCapacity || (team.Users.Count + 1 >= 3 && project.TeamCapacity >= 4))
            {
                if (team.TeamStatus == TeamStatus.Incomplete)
                {
                    team.GetReadyForEntry();
                    team.UpdateTeamCompleteDate(DateTimeHelper.GetCurrentTime());
                }



                //if (team.Users.Count >= project.TeamCapacity - 1)
                //{
                //    if (team.TeamStatus == TeamStatus.Incomplete)
                //    {

                //        team.GetReadyForEntry();
                //        team.UpdateTeamCompleteDate(DateTimeHelper.GetCurrentTime());
                //    }

                //    // if (!team.AuditoriumId.HasValue)
                //    // {
                //    //     var availableAuditoriums = await _auditoriumRepository.ListAsync(
                //    //         AuditoriumSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);
                //    //     var teamsWithAuditoriums = await _teamRepository.ListAsync(
                //    //         TeamSpecification.GetTeamsWithAuditorium(_profileProvider.GetProfile().User.EventId),
                //    //         cancellationToken);
                //    //
                //    //     var auditorium = availableAuditoriums.FirstOrDefault(x =>
                //    //                          x.IsActive && teamsWithAuditoriums.Count(a => a.AuditoriumId == x.Key) < x.Capacity) ??
                //    //                      availableAuditoriums.FirstOrDefault(x => x.IsDefaultAuditory);
                //    //     team.UpdateAuditorium(auditorium?.Key);
                //    // }
                //}
            }

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}