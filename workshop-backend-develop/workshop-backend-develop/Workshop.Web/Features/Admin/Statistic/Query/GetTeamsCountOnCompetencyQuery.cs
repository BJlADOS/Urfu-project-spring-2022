using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Specification;
using Workshop.Web.Dtos.Admin.Statistic;

namespace Workshop.Web.Features.Admin.Statistic.Query
{
    public class GetTeamsCountOnCompetencyQuery : IRequest<ICollection<TeamsCountOnCompetencyDto>>
    {
        public long EventId { get; set; }
    }

    public class GetTeamsCountOnCompetencyQueryHandler : IRequestHandler<GetTeamsCountOnCompetencyQuery,
        ICollection<TeamsCountOnCompetencyDto>>

    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITeamRepository _teamRepository;

        public GetTeamsCountOnCompetencyQueryHandler(ITeamRepository teamRepository,
            IProjectRepository projectRepository)
        {
            _teamRepository = teamRepository;
            _projectRepository = projectRepository;
        }

        public async Task<ICollection<TeamsCountOnCompetencyDto>> Handle(GetTeamsCountOnCompetencyQuery request,
            CancellationToken cancellationToken)
        {
            await _projectRepository.ListAsync(ProjectSpecification.GetAll(request.EventId), cancellationToken);

            var teams = await _teamRepository.ListAsync(TeamSpecification.GetAll(request.EventId), cancellationToken);

            var competenciesStats = new Dictionary<string, int>();

            foreach (var competency in teams.Select(t => t.Project).SelectMany(p => p.Competencies))
            {
                var name = competency.Competency.Name;
                if (competenciesStats.ContainsKey(name))
                    competenciesStats[name]++;
                else
                    competenciesStats.Add(name, 1);
            }

            var teamsCountOnCompetencyDtos = competenciesStats.Select(p => new TeamsCountOnCompetencyDto
            {
                Competency = p.Key,
                Value = p.Value
            }).ToArray();

            return teamsCountOnCompetencyDtos;
        }
    }
}