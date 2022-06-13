using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Infrastructure.Storage.Repositories;
using Aspose.Cells;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.ProjectProposal;
using Workshop.Core.Domain.Specification;
using Workshop.Web.Dtos.Expert.Project;
using Workshop.Web.Features.Admin.Helpers;

namespace Workshop.Web.Features.Admin.GenerateData
{
    public class GenerateDataCommand : IRequest
    {
        public GenerateProjectsDto GenerateProjectsDto { get; set; }
    }

    public class GenerateDataCommandHandler : AsyncRequestHandler<GenerateDataCommand>
    {
        private CompetencyRepository _competencyRepository;
        private LifeScenarioRepository _lifeScenarioRepository;
        private KeyTechnologyRepository _keyTechnologyRepository;
        private ProjectRepository _projectRepository;
        private TeamRepository _teamRepository;
        private RoleRepository _roleRepository;
        public GenerateDataCommandHandler(CompetencyRepository competencyRepository,
            LifeScenarioRepository lifeScenarioRepository,
            KeyTechnologyRepository keyTechnologyRepository, ProjectRepository projectRepository,
            TeamRepository teamRepository,
            RoleRepository roleRepository)
        {
            _competencyRepository = competencyRepository;
            _lifeScenarioRepository = lifeScenarioRepository;
            _keyTechnologyRepository = keyTechnologyRepository;
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
            _roleRepository = roleRepository;
        }

        protected override async Task Handle(GenerateDataCommand request, CancellationToken cancellationToken)
        {
            var eventId = request.GenerateProjectsDto.EventId;

            if (request.GenerateProjectsDto.ResetProjects)
            {
                await ResetProjects(eventId, cancellationToken);
            }

            var projectDatas = await GetDataFromFile(request);

            var competencies = await CommandHelper
                .UpdateCompetencies(_competencyRepository, projectDatas
                                        .SelectMany(x => x.SoftSkills),
                projectDatas.SelectMany(x => x.HardSkills), cancellationToken);

            var projects = await UpdateProjects(eventId,
                                                projectDatas, request.GenerateProjectsDto.CloseProjects, cancellationToken);

            UpdateProjectCompetencies(projectDatas, projects, competencies);
            await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        private static void UpdateProjectCompetencies(
            IEnumerable<ProjectData> dataProjects,
            Dictionary<string, Core.Domain.Model.Project.Project> projects,
            Dictionary<string, Competency> competencies)
        {
            foreach (var projectData in dataProjects)
            {
                var project = projects[projectData.ProjectName];
                var listCompetencies = CommandHelper.GetProjectCompetencies(project.Key,
                    projectData.SoftSkills, projectData.HardSkills, competencies);
                project.UpdateCompetencies(listCompetencies);
            }
        }

        private async Task ResetProjects(long eventId, CancellationToken cancellationToken)
        {
            var projectsToRemove = await _projectRepository.ListAsync(ProjectSpecification.GetAll(eventId), cancellationToken);
            var teamsToRemove = await _teamRepository.ListAsync(TeamSpecification.GetAll(eventId), cancellationToken);
            var keyTechnologiesToRemove = await _keyTechnologyRepository.ListAsync(KeyTechnologySpecification.GetAll(eventId), cancellationToken);
            var lifeScenariosToRemove = await _lifeScenarioRepository.ListAsync(LifeScenarioSpecification.GetAll(eventId), cancellationToken);

            if (projectsToRemove != null && projectsToRemove.Length > 0)
                await _projectRepository.RemoveRangeAsync(projectsToRemove);

            if (teamsToRemove != null && teamsToRemove.Length > 0)
                await _teamRepository.RemoveRangeAsync(teamsToRemove);

            if (lifeScenariosToRemove != null && lifeScenariosToRemove.Length > 0)
                await _lifeScenarioRepository.RemoveRangeAsync(lifeScenariosToRemove);

            if (keyTechnologiesToRemove != null && keyTechnologiesToRemove.Length > 0)
                await _keyTechnologyRepository.RemoveRangeAsync(keyTechnologiesToRemove);
        }

        private async Task<Dictionary<string, Core.Domain.Model.Project.Project>> UpdateProjects(long eventId, List<ProjectData> projectDatas,
            bool closeProjects,
            CancellationToken cancellationToken)
        {
            var lifeScenarios = await CommandHelper.UpdateLifeScenarios(_lifeScenarioRepository, eventId,
                projectDatas.Select(x => x.LifeScenario), cancellationToken);

            var keyTechnology = await CommandHelper.UpdateKeyTechnologies(_keyTechnologyRepository, eventId,
                projectDatas.Select(x => x.KeyTechnology), cancellationToken);

            var newProjects = new Dictionary<string, Core.Domain.Model.Project.Project>();
            //var newTeams = new List<Core.Domain.Model.Team.Team>();

            foreach (var projectData in projectDatas)
            {
                var newProjectData = new ProjectData
                {
                    ProjectName = projectData.ProjectName,
                    Description = projectData.Description,
                    Curator = projectData.Curator,
                    Organization = projectData.Organization,
                    Contacts = projectData.Contacts,
                    Purpose = projectData.Purpose,
                    Result = projectData.Result,
                    TeamsSize = projectData.TeamsSize,
                    LifeScenarioId = lifeScenarios[projectData.LifeScenario].Key,
                    KeyTechnologyId = keyTechnology[projectData.KeyTechnology].Key,
                    EventId = eventId,
                    TeamLimit = projectData.TeamLimit,
                    IsAvailable = !closeProjects
                };

                var project = new Core.Domain.Model.Project.Project(newProjectData);
                if (newProjects.ContainsKey(project.Name))
                {
                    continue;
                }

                newProjects.Add(project.Name, project);
                await _projectRepository.AddAsync(project, cancellationToken);

                var roles = projectData.Roles?.Split(",", StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Append("Тимлид")
                    .ToArray();

                await CommandHelper.UpdateRoles(_roleRepository, roles, project, cancellationToken);
            }

            await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return newProjects;
        }

        private static async Task<List<ProjectData>> GetDataFromFile(GenerateDataCommand request)
        {
            var projectDatas = new List<ProjectData>();
            await using var ms = new MemoryStream();
            await request.GenerateProjectsDto.File.CopyToAsync(ms);
            var workbook = new Workbook(ms, new LoadOptions(LoadFormat.Xlsx));
            var worksheet = workbook.Worksheets[0];

            var cellTypes = Enum.GetValues(typeof(CellType))
                                .Cast<CellType>()
                                .ToArray();

            for (var i = 1; i < worksheet.Cells.MaxDataRow + 1; i++)
            {
                var projectData = new Dictionary<CellType, Cell>(cellTypes.Length);
                for (var j = 0; j < cellTypes.Length; j++)
                {
                    projectData.Add(cellTypes[j], worksheet.Cells[i, j]);
                }

                var project = new ProjectData(projectData);

                projectDatas.Add(project);
            }

            return projectDatas;
        }
    }
}