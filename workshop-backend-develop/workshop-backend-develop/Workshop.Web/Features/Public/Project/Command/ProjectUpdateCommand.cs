using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.ProjectCompetency;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Public.Project.Command
{
    public class ProjectUpdateCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Curator { get; set; }
        public string Organization { get; set; }
        public string Contacts { get; set; }
        public string Purpose { get; set; }
        public string Result { get; set; }
        public long[] CompetenciesIds { get; set; }
        public long LifeScenarioId { get; set; }
        public long KeyTechnologyId { get; set; }
        public bool IsPromoted { get; set; }
        public bool IsAvailable { get; set; }

        public int MaxTeamCount { get; set; }
    }

    public class ProjectUpdateCommandHandler : AsyncRequestHandler<ProjectUpdateCommand>
    {
        private IUserProfileProvider _profileProvider;
        private IProjectRepository _projectRepository;
        private ITeamRepository _teamRepository;

        public ProjectUpdateCommandHandler(IProjectRepository projectRepository, IUserProfileProvider profileProvider,
            ITeamRepository teamRepository)
        {
            _profileProvider = profileProvider;
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
        }

        protected override async Task Handle(ProjectUpdateCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.SingleAsync(
                ProjectSpecification.GetById(request.Id, _profileProvider.GetProfile().User.EventId),
                cancellationToken);
            project.UpdateTextData(
                request.Name,
                request.Description,
                request.Curator,
                request.Organization,
                request.Contacts,
                request.Purpose,
                request.Result
            );

            project.UpdateMaxTeamsCount(request.MaxTeamCount);

            project.UpdateLifeScenario(request.LifeScenarioId);
            project.UpdateKeyTechnology(request.KeyTechnologyId);

            if (request.CompetenciesIds != null)
            {
                var competenties = CreateProjectCompetencies(request.Id, request.CompetenciesIds);
                project.UpdateCompetencies(competenties);
            }

            if (request.IsAvailable != project.IsAvailable)
                project.UpdateAvailability(request.IsAvailable);

            project.SetProjectPromotion(request.IsPromoted);

            await _projectRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        private List<ProjectCompetency> CreateProjectCompetencies(long projectId, long[] competenciesIds)
        {
            return competenciesIds.Select(id => new ProjectCompetency(projectId, id)).ToList();
        }
    }
}