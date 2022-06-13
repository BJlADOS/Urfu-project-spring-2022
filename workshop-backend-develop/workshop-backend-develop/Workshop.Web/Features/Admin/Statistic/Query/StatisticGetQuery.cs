using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.KeyTechnology;
using Workshop.Core.Domain.Model.LifeScenario;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Admin.Statistic;
using Workshop.Web.Dtos.Public.KeyTechnology;
using Workshop.Web.Dtos.Public.LifeScenario;

namespace Workshop.Web.Features.Admin.Statistic.Query
{
    public class StatisticGetQuery : IRequest<ICollection<StatisticDto>>
    {
    }

    public class StatisticGetQueryHandler : IRequestHandler<StatisticGetQuery, ICollection<StatisticDto>>
    {
        private IReadOnlyRepository<LifeScenario> _lifeScenarioRepository;
        private IReadOnlyRepository<KeyTechnology> _keyTechnologyRepository;
        private IReadOnlyRepository<Core.Domain.Model.Team.Team> _teamRepository;
        private IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;
        private IReadOnlyRepository<Core.Domain.Model.User.User> _userRepository;
        private IMapper _mapper;
        private IUserProfileProvider _profileProvider;

        public StatisticGetQueryHandler(IReadOnlyRepository<LifeScenario> lifeScenarioRepository, IReadOnlyRepository<KeyTechnology> keyTechnologyRepository, IReadOnlyRepository<Core.Domain.Model.Team.Team> teamRepository, IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository, IMapper mapper, IUserProfileProvider profileProvider, IReadOnlyRepository<Core.Domain.Model.User.User> userRepository)
        {
            _lifeScenarioRepository = lifeScenarioRepository;
            _keyTechnologyRepository = keyTechnologyRepository;
            _teamRepository = teamRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
            _profileProvider = profileProvider;
            _userRepository = userRepository;
        }

        public async Task<ICollection<StatisticDto>> Handle(StatisticGetQuery request, CancellationToken cancellationToken)
        {
            var lifeScenarios = await _lifeScenarioRepository.ListAsync(
                LifeScenarioSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);

            var keyTechnologies = await _keyTechnologyRepository.ListAsync(
               KeyTechnologySpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);

            var projects = await _projectRepository.ListAsync(
                ProjectSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);

            var teams = await _teamRepository.ListAsync(TeamSpecification.GetNotEmpty(_profileProvider.GetProfile().User.EventId),
                cancellationToken);

            var users = await _userRepository.ListAsync(
                UserSpecification.GetAllStudents(_profileProvider.GetProfile().User.EventId), cancellationToken);

            var statisticItems = new List<StatisticDto>();

            foreach (var lifeScenario in lifeScenarios)
            {
                var technologies = new List<KeyTechnology>();
                foreach (var project in projects)
                    if (project.LifeScenarioId == lifeScenario.Key && !technologies.Select(t => t.Key).Contains(project.KeyTechnologyId))
                    {
                        technologies.Add(project.KeyTechnology);
                    }
                technologies.Sort((first, second) => first.Key.CompareTo(second.Key));
                statisticItems.AddRange(technologies.Select(x =>
                {
                    var statisticItemProjects = projects.Where(p =>
                        p.LifeScenarioId == lifeScenario.Key && p.KeyTechnologyId == x.Key);

                    var teamsCount = teams.Count(t => statisticItemProjects.Any(s => s.Key == t.ProjectId));
                    var usersCount = users.Count(u => statisticItemProjects.Any(s => u.TeamId.HasValue && s.Key == u.Team.ProjectId));

                    return new StatisticDto()
                    {
                        LifeScenario = _mapper.Map<LifeScenarioDto>(lifeScenario),
                        KeyTechnology = _mapper.Map<KeyTechnologyDto>(x),
                        TeamsCount = teamsCount,
                        StudentsCount = usersCount
                    };
                }));
            }

            return statisticItems;
        }
    }
}
