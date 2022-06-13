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
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.TeamLead.Command
{
    public class AddNewTeamCommand : IRequest
    {
        public long ProjectId { get; set; }
    }

    public class AddNewTeamCommandHandler : AsyncRequestHandler<AddNewTeamCommand>
    {
        private ITeamRepository _teamRepository;
        private IProjectRepository _projectRepository;
        private IUserProfileProvider _profileProvider;
        private UserRepository _userRepository;

        public AddNewTeamCommandHandler(ITeamRepository teamRepository, IProjectRepository projectRepository,
            IUserProfileProvider profileProvider,
            UserRepository userRepository)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        protected override async Task Handle(AddNewTeamCommand request, CancellationToken cancellationToken)
        {
            //var user = _profileProvider.GetProfile().User;
            var user = await _userRepository.SingleAsync(UserSpecification.GetById(_profileProvider.GetProfile().User.Key), cancellationToken);
            var eventId = user.EventId;
            var project = await _projectRepository.SingleOrDefaultAsync(
                ProjectSpecification.GetById(request.ProjectId, eventId),
                cancellationToken);

            if(project is null)
            {
                throw new NotFoundException();
            }

            //project.IncrementMaxTeamsCount();

            var newTeam = new Team(project, eventId);
            

            await _teamRepository.AddAsync(newTeam, cancellationToken);
            
            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            user.UpdateTeam(newTeam.Key);
            user.UpdateRole(project.Roles.Last().Key);
            //await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
