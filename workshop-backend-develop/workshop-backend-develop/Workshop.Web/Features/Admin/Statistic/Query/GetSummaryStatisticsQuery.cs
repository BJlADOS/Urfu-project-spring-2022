using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.Project;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Web.Dtos.Admin.Statistic;

namespace Workshop.Web.Features.Admin.Statistic.Query
{
    public class GetSummaryStatisticsQuery : IRequest<SummaryStatisticsDto>
    {
        public long EventId { get; set; }
    }

    public class GetSummaryStatisticsQueryHandler : IRequestHandler<GetSummaryStatisticsQuery, SummaryStatisticsDto>
    {
        private readonly IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;
        private readonly IReadOnlyRepository<Core.Domain.Model.Team.Team> _teamRepository;
        private readonly IReadOnlyRepository<Core.Domain.Model.User.User> _userRepository;

        public GetSummaryStatisticsQueryHandler(IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository, IReadOnlyRepository<Core.Domain.Model.Team.Team> teamRepository,
            IReadOnlyRepository<Core.Domain.Model.User.User> userRepository)
        {
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
        }

        public async Task<SummaryStatisticsDto> Handle(GetSummaryStatisticsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.ListAsync(ProjectSpecification.GetAll(request.EventId), cancellationToken);
            var teams = await _teamRepository.ListAsync(TeamSpecification.GetAllCompleted(request.EventId), cancellationToken);
            var users = await _userRepository.ListAsync(UserSpecification.GetAllWithFilledProfile(request.EventId), cancellationToken);

            var summaryStatisticsDto = new SummaryStatisticsDto
            {
                StudentsCount = users.Count(),
                ProjectsCount = projects.Length,
                TeamsCount = teams.Count(),
                CuratorsCount = projects.Select(p => p.Curator).Distinct().Count(),
                CustomersCount = projects.Select(p => p.Organization).Distinct().Count()
            };

            return summaryStatisticsDto;
        }
    }
}