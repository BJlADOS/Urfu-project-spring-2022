using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.ProjectProposal;
using Workshop.Web.Exceptions;
using Workshop.Web.Features.Admin.Helpers;

namespace Workshop.Web.Features.Public.ProjectProposal.Command
{
    public class CreateProjectFromProposalByIdCommand : IRequest
    {
        public string[] RoleNames { get; set; }
        public long ProposalId { get; set; }
        public CreateProjectFromProposalDto CreateProjectDto { get; set; }
    }

    public class CreateProjectFromProposalByIdCommandHandler : AsyncRequestHandler<CreateProjectFromProposalByIdCommand>
    {
        private readonly IUserProfileProvider _profileProvider;
        private readonly ProjectProposalRepository _projectProposalRepository;
        private readonly ProjectRepository _projectRepository;
        private readonly TeamRepository _teamRepository;
        private readonly RoleRepository _roleRepository;
        private readonly LifeScenarioRepository _lifeScenarioRepository;
        private readonly KeyTechnologyRepository _keyTechnologyRepository;
        private readonly UserRepository _userRepository;

        public CreateProjectFromProposalByIdCommandHandler(IUserProfileProvider profile,
            ProjectProposalRepository projectProposals,
            ProjectRepository projects,
            TeamRepository teams,
            RoleRepository roles,
            LifeScenarioRepository lifeScenario,
            KeyTechnologyRepository keyTechnology,
            UserRepository users)
        {
            _profileProvider = profile;
            _projectProposalRepository = projectProposals;
            _projectRepository = projects;
            _teamRepository = teams;
            _roleRepository = roles;
            _lifeScenarioRepository = lifeScenario;
            _keyTechnologyRepository = keyTechnology;
            _userRepository = users;
        }

        protected override async Task Handle(CreateProjectFromProposalByIdCommand request,
            CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;

            var proposal = await _projectProposalRepository.FirstOrDefaultAsync(
                ProjectProposalSpecification.GetById(request.ProposalId, user.EventId), cancellationToken);
            var authorUser = await _userRepository.SingleOrDefaultAsync(UserSpecification.GetById(proposal.AuthorId),
                cancellationToken);

            if (proposal is null || authorUser is null)
            {
                throw new NotFoundException();
            }

            proposal.UpdateData(request.CreateProjectDto);


            var proposals = await _projectProposalRepository.ListAsync(
                ProjectProposalSpecification.GetByAuthorId(user.EventId, proposal.AuthorId), cancellationToken);

            var lifeScenarios = await CommandHelper.UpdateLifeScenarios(_lifeScenarioRepository, user.EventId,
                new[] {request.CreateProjectDto.LifeScenarioName}, cancellationToken);

            var keyTechnologies = await CommandHelper.UpdateKeyTechnologies(_keyTechnologyRepository, user.EventId,
                new[] {request.CreateProjectDto.KeyTechnologyName}, cancellationToken);

            var project = new Core.Domain.Model.Project.Project(
                proposal,
                lifeScenarios[request.CreateProjectDto.LifeScenarioName].Key,
                keyTechnologies[request.CreateProjectDto.KeyTechnologyName].Key);

            await _projectRepository.AddAsync(project, cancellationToken);
            await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            await _projectProposalRepository.RemoveRangeAsync(proposals);
            await _projectProposalRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var roles = new List<string>() {"Тимлид"};
            roles.AddRange(request.RoleNames);

            await CommandHelper.UpdateRoles(_roleRepository, roles, project, cancellationToken);
            await _roleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);


            var team = new Core.Domain.Model.Team.Team(project, user.EventId);
            team.UpdateEntried(true);
            team.CompleteTest();

            await _teamRepository.AddAsync(team, cancellationToken);
            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            if (authorUser.TeamId == null)
            {
                authorUser.UpdateTeam(team.Key);
                authorUser.UpdateRole(project.Roles.First(r => r.Name == "Тимлид").Key);
            }

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}