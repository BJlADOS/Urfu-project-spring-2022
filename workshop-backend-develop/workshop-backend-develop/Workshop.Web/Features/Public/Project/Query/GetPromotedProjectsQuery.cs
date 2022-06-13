using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Public.Competency;
using Workshop.Web.Dtos.Public.Project;

namespace Workshop.Web.Features.Public.Project.Query
{
    public class GetPromotedProjectsQuery : IRequest<ShortProjectDto[]>
    {
    }

    public class GetPromotedProjectsQueryHandler : IRequestHandler<GetPromotedProjectsQuery, ShortProjectDto[]>
    {
        private readonly IMapper _mapper;
        private IReadOnlyRepository<Core.Domain.Model.Project.Project> _projectRepository;
        private IReadOnlyRepository<Core.Domain.Model.User.User> _userRepository;
        private IReadOnlyRepository<Core.Domain.Model.Team.Team> _teamRepository;
        private IUserProfileProvider _profileProvider;

        public GetPromotedProjectsQueryHandler(
            IReadOnlyRepository<Core.Domain.Model.Project.Project> projectRepository,
            IMapper mapper, IReadOnlyRepository<Core.Domain.Model.User.User> userRepository,
            IReadOnlyRepository<Core.Domain.Model.Team.Team> teamRepository, IUserProfileProvider profileProvider)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
        }

        public async Task<ShortProjectDto[]> Handle(GetPromotedProjectsQuery request, CancellationToken cancellationToken)
        {
            var projects = await _projectRepository.ListAsync(
                ProjectSpecification.GetPromoted(_profileProvider.GetProfile().User.EventId), cancellationToken);
            var user = await _userRepository.SingleAsync(
                UserSpecification.GetById(_profileProvider.GetProfile().User.Key), cancellationToken);
            var teams = await _teamRepository.ListAsync(
                TeamSpecification.GetAll(_profileProvider.GetProfile().User.EventId), cancellationToken);

            if (user.Team != null)
            {
                var choosedProject = projects.FirstOrDefault(x => x.Key == user.Team.ProjectId);
                if (choosedProject != null)
                {
                    projects = projects.Except(new[] { choosedProject }).ToArray();
                }
            }

            var projectDtoList = new List<ShortProjectDto>();
            foreach (var project in projects)
            {
                var competencies = _mapper.Map<ICollection<CompetencyDto>>(project.Competencies);
                var fillTeamsCount = 0;
                var participantsCount = 0;
                foreach (var team in teams.Where(x => x.ProjectId == project.Key))
                {
                    participantsCount += team.Users.Count;
                    fillTeamsCount += team.TeamStatus != TeamStatus.Incomplete ? 1 : 0;
                }
                var projectDto = _mapper.Map<ShortProjectDto>(project);
                projectDto.Competencies = competencies;

                projectDto.FillTeamsCount = fillTeamsCount;
                projectDto.ParticipantsCount = participantsCount;
                projectDtoList.Add(projectDto);
            }

            return projectDtoList.ToArray();
        }
    }
}
