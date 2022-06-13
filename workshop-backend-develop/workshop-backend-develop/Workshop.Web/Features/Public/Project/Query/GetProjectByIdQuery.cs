using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.LifeScenario;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.LifeScenario;
using Workshop.Web.Dtos.Public.Project;
using Workshop.Web.Dtos.Public.Team;
using Workshop.Web.Features.Public.LifeScenarios.Query;

namespace Workshop.Web.Features.Public.Project.Query
{
    public class GetProjectByIdQuery : IRequest<ProjectDto>
    {
        public long Id { get; set; }
    }

    public class GetProjectQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly IMapper _mapper;
        private IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;
        private IReadOnlyRepository<Core.Domain.Model.User.User> _userRepository;
        private IReadOnlyRepository<Core.Domain.Model.Team.Team> _teamRepository;
        private IUserProfileProvider _profileProvider;

        public GetProjectQueryHandler(IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository,
            IMapper mapper, IReadOnlyRepository<Core.Domain.Model.Team.Team> teamRepository,
            IReadOnlyRepository<Core.Domain.Model.User.User> userRepository, IUserProfileProvider profileProvider)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _profileProvider = profileProvider;
            _projectRepository = projectRepository;
        }

        public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.SingleAsync(
                ProjectSpecification.GetById(request.Id, _profileProvider.GetProfile().User.EventId),
                cancellationToken);

            var teams = await _teamRepository.ListAsync(
                TeamSpecification.GetProjectTeams(request.Id, _profileProvider.GetProfile().User.EventId),
                cancellationToken);

            var user = await _userRepository.SingleAsync(
                UserSpecification.GetById(_profileProvider.GetProfile().User.Key), cancellationToken);

            var projectDto = _mapper.Map<ProjectDto>(project);
            var competencies = _mapper.Map<ICollection<CompetencyDto>>(project.Competencies);

            projectDto.Competencies = competencies;
            projectDto.MatchedCompetenciesCount = project.Competencies.Count(pc =>
                user.Competencies.Any(uc => uc.CompetencyId == pc.CompetencyId));

            var teamDtos = _mapper.Map<ICollection<ShortTeamDto>>(teams
                .OrderBy(x => x.TeamStatus != TeamStatus.Incomplete).ThenByDescending(x => x.Users.Count));
            projectDto.Teams = teamDtos;
            projectDto.FillTeamsCount = teams.Count(x => x.TeamStatus == TeamStatus.Complete);

            return projectDto;
        }
    }
}