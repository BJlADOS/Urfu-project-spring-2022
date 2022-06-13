using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Admin.Project;
using Workshop.Web.Features.Admin.Helpers;

namespace Workshop.Web.Features.Admin.Project.Command
{
    public class CreateProjectCommand : IRequest<long>
    {
        public CreateProjectDto CreateProjectDto { get; set; }
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, long>
    {
        private readonly ProjectRepository _projectRepository;
        private readonly LifeScenarioRepository _lifeScenarioRepository;
        private readonly KeyTechnologyRepository _keyTechnologyRepository;
        private readonly IUserProfileProvider _profileProvider;
        private readonly CompetencyRepository _competencyRepository;
        private readonly RoleRepository _roleRepository;
        private readonly TeamRepository _teamRepository;

        public CreateProjectCommandHandler(
            ProjectRepository projectRepository,
            LifeScenarioRepository lifeScenarioRepository,
            KeyTechnologyRepository keyTechnologyRepository,
            IUserProfileProvider profileProvider,
            CompetencyRepository competencyRepository,
            TeamRepository teamRepository,
            RoleRepository roleRepository)
        {
            _projectRepository = projectRepository;
            _lifeScenarioRepository = lifeScenarioRepository;
            _keyTechnologyRepository = keyTechnologyRepository;
            _profileProvider = profileProvider;
            _competencyRepository = competencyRepository;
            _roleRepository = roleRepository;
            _teamRepository = teamRepository;
        }

        public async Task<long> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var eventId = _profileProvider.GetProfile().User.EventId;

            var lifeScenarios = await CommandHelper.UpdateLifeScenarios(_lifeScenarioRepository, eventId,
                new[] {request.CreateProjectDto.LifeScenarioName}, cancellationToken);

            var keyTechnologies = await CommandHelper.UpdateKeyTechnologies(_keyTechnologyRepository, eventId,
                new[] {request.CreateProjectDto.KeyTechnologyName}, cancellationToken);

            var competencies = await CommandHelper.UpdateCompetencies(_competencyRepository,
                request.CreateProjectDto.SoftSkills, request.CreateProjectDto.HardSkills, cancellationToken);

            var projectData = new ProjectData
            {
                ProjectName = request.CreateProjectDto.Name,
                Description = request.CreateProjectDto.Description,
                Curator = request.CreateProjectDto.Curator,
                Organization = request.CreateProjectDto.Organization,
                Contacts = request.CreateProjectDto.Contacts,
                Purpose = request.CreateProjectDto.Purpose,
                Result = request.CreateProjectDto.Result,
                TeamsSize = request.CreateProjectDto.TeamsSize,
                LifeScenarioId = lifeScenarios[request.CreateProjectDto.LifeScenarioName].Key,
                KeyTechnologyId = keyTechnologies[request.CreateProjectDto.KeyTechnologyName].Key,
                EventId = eventId,
                TeamLimit = request.CreateProjectDto.TeamLimit,
                IsAvailable = true,
                HardSkills = request.CreateProjectDto.HardSkills,
                SoftSkills = request.CreateProjectDto.SoftSkills
            };

            var project = new Core.Domain.Model.Project.Project(projectData);
            await _projectRepository.AddAsync(project, cancellationToken);
            await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            var projectCompetencies = CommandHelper.GetProjectCompetencies(project.Key,
                projectData.SoftSkills, projectData.HardSkills, competencies);
            project.UpdateCompetencies(projectCompetencies);

            var currentRoles = request.CreateProjectDto.Roles.Append("Тимлид").ToArray();
            await CommandHelper.UpdateRoles(_roleRepository, currentRoles, project, cancellationToken);
            await _roleRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

            return project.Key;
        }

        private async Task UpdateProjectTeams(Core.Domain.Model.Project.Project project,
            CancellationToken cancellationToken)
        {
            var newTeams = new List<Core.Domain.Model.Team.Team>();

            for (var i = 0; i < project.MaxTeamCount; i++)
            {
                var newTeam = new Core.Domain.Model.Team.Team(project, project.EventId);
                newTeams.Add(newTeam);
            }

            await _teamRepository.AddRangeAsync(newTeams, cancellationToken);
        }
    }
}