using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.Project;

namespace Workshop.Web.Features.Public.Project.Query
{
    public class GetProjectsFilteredQuery : IRequest<ShortProjectDto[]>
    {
        public GetProjectsDto GetProjectsDto { get; set; }
    }

    public class GetProjectByTechLifeQueryHandler : IRequestHandler<GetProjectsFilteredQuery, ShortProjectDto[]>
    {
        private readonly IMapper _mapper;
        private ProjectRepository _projectRepository;
        private IReadOnlyRepository<Core.Domain.Model.User.User> _userRepository;
        private TeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public GetProjectByTechLifeQueryHandler(
            ProjectRepository projectRepository,
            IMapper mapper, IReadOnlyRepository<Core.Domain.Model.User.User> userRepository,
            TeamRepository teamRepository, IUserProfileProvider profileProvider)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
        }

        public async Task<ShortProjectDto[]> Handle(GetProjectsFilteredQuery request,
            CancellationToken cancellationToken)
        {
            var filters = request.GetProjectsDto;
            var projects = await _projectRepository.ShallowListAsync(
                ProjectSpecification.GetByQuery(filters.KeyTechnologyId, filters.LifeScenarioId, filters.Curator,
                    filters.Customer, filters.Term,
                    _profileProvider.GetProfile().User.EventId), cancellationToken);
            var user = await _userRepository.SingleAsync(
                UserSpecification.GetById(_profileProvider.GetProfile().User.Key), cancellationToken);
            var teams = await _teamRepository.ShallowListAsync(
                TeamSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);

            var projectDtoList = new List<ShortProjectDto>();
            foreach (var project in projects)
            {
                var fillTeamsCount = 0;
                var participantsCount = 0;
                foreach (var team in teams.Where(x => x.ProjectId == project.Key))
                {
                    participantsCount += team.Users.Count;
                    fillTeamsCount += team.TeamStatus != TeamStatus.Incomplete && team.IsEntried ? 1 : 0;
                }

                var projectDto = _mapper.Map<ShortProjectDto>(project);
                projectDto.MaxTeamCount = project.MaxTeamCount;
                projectDto.FillTeamsCount = fillTeamsCount;
                projectDto.ParticipantsCount = participantsCount;
                projectDtoList.Add(projectDto);
            }

            return projectDtoList.ToArray();
        }
    }
}