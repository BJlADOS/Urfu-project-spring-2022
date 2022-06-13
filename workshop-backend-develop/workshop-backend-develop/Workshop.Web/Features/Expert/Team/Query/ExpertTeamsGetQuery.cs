using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Dtos.Public.Team;

namespace Workshop.Web.Features.Expert.Team.Query
{
    public class ExpertTeamsGetQuery : IRequest<ICollection<TeamListItemDto>>
    {
        public string Term { get; set; }
    }

    public class ExpertTeamsGetQueryHandler : IRequestHandler<ExpertTeamsGetQuery, ICollection<TeamListItemDto>>
    {
        private IMapper _mapper;
        private TeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public ExpertTeamsGetQueryHandler(TeamRepository teamRepository, IMapper mapper, IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _profileProvider = profileProvider;
        }

        public async Task<ICollection<TeamListItemDto>> Handle(ExpertTeamsGetQuery request, CancellationToken cancellationToken)
        {
            var teams = await _teamRepository.ShallowListAsync(TeamSpecification.GetAllRegistred(_profileProvider.GetProfile().User.EventId), cancellationToken);

            teams = teams
                 .Where(x => string.IsNullOrWhiteSpace(request.Term)
                             || x.Key.ToString().Contains(request.Term, StringComparison.InvariantCultureIgnoreCase)
                             || x.Project.Name.Contains(request.Term, StringComparison.InvariantCultureIgnoreCase)
                             || (x.Name?.Contains(request.Term, StringComparison.InvariantCultureIgnoreCase) ?? false))
                 .OrderBy(x => x.TeamStatus == TeamStatus.Incomplete)
                 .ThenByDescending(x => string.IsNullOrWhiteSpace(request.Term) // вперед выдаем поиск по номеру команды
                                        || x.Key.ToString().Contains(request.Term, StringComparison.InvariantCultureIgnoreCase))
                 .ThenByDescending(x => x.Users.Count)
                 .ToArray();

            var teamDtos = new List<TeamListItemDto>();

            foreach (var team in teams)
            {
                var teamDto = _mapper.Map<TeamListItemDto>(team);

                teamDtos.Add(teamDto);
            }

            return teamDtos;
        }
    }
}