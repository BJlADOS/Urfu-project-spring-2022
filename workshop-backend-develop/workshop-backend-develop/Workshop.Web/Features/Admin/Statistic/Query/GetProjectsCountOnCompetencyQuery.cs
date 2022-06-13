using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Project;
using Workshop.Web.Dtos.Admin.Statistic;

namespace Workshop.Web.Features.Admin.Statistic.Query
{
    public class GetProjectsCountOnCompetencyQuery : IRequest<ICollection<CompetenciesCountDto>>
    {
        public long EventId { get; set; }
    }

    public class GetProjectsCountOnCompetencyQueryHandler : IRequestHandler<GetProjectsCountOnCompetencyQuery, ICollection<CompetenciesCountDto>>
    {
        private readonly IProjectRepository _projectRepository;

        public GetProjectsCountOnCompetencyQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ICollection<CompetenciesCountDto>> Handle(GetProjectsCountOnCompetencyQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.ListAsync(cancellationToken);

            var competencies = projects
                .SelectMany(u => u.Competencies)
                .Select(c => c.Competency);

            var competenciesStats = new Dictionary<long, CompetenciesCountDto>();

            foreach (var competency in competencies)
            {
                if (competenciesStats.ContainsKey(competency.Key))
                {
                    competenciesStats[competency.Key].Value++;
                }
                else
                {
                    competenciesStats.Add(competency.Key, new CompetenciesCountDto
                    {
                        Competency = competency.Name,
                        CompetencyType = competency.CompetencyType.ToString(),
                        Value = 1,
                    });
                }
            }

            return competenciesStats.Values;
        }
    }
}
