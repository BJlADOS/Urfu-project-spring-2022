using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Model.UserCompetency;
using Workshop.Core.Domain.Specification;
using Workshop.Web.Dtos.Admin.Statistic;

namespace Workshop.Web.Features.Admin.Statistic.Query
{
    public class GetStudentsCountOnCompetencyQuery : IRequest<ICollection<CompetenciesCountDto>>
    {
        public long EventId { get; set; }
    }

    public class GetStudentsCountOnCompetencyQueryHandler : IRequestHandler<GetStudentsCountOnCompetencyQuery, ICollection<CompetenciesCountDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetStudentsCountOnCompetencyQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ICollection<CompetenciesCountDto>> Handle(GetStudentsCountOnCompetencyQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.ListAsync(UserSpecification.GetAllStudents(request.EventId), cancellationToken);

            var competencies = users
                .SelectMany(u => u.Competencies)
                .Where(c => c.UserCompetencyType == UserCompetencyType.Current)
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
