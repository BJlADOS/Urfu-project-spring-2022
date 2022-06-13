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
using Workshop.Web.Dtos.Expert.Team;
using Workshop.Web.Dtos.Public.Team;

namespace Workshop.Web.Features.Expert.Team.Query
{
    public class ExpertReviewTeamsGetQuery : IRequest<ICollection<TeamWithSlotListDto>>
    {
        public string Term { get; set; }
    }

    public class ExpertReviewTeamsGetQueryHandler : IRequestHandler<ExpertReviewTeamsGetQuery, ICollection<TeamWithSlotListDto>>
    {
        private IMapper _mapper;
        private TeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public ExpertReviewTeamsGetQueryHandler(TeamRepository teamRepository, IMapper mapper, IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
            _profileProvider = profileProvider;
        }

        public async Task<ICollection<TeamWithSlotListDto>> Handle(ExpertReviewTeamsGetQuery request, CancellationToken cancellationToken)
        {
            var user = _profileProvider.GetProfile().User;
            var eventId = user.EventId;

            var teamDtos = new List<TeamWithSlotListDto>();

            foreach (var auditorium in user.Auditoriums)
            {
                var teams = await _teamRepository.ListWithSlotsAsync(
                    TeamSpecification.GetTeamsByAuditoriumId(eventId, auditorium.AuditoriumId),
                    cancellationToken
                    );

                teams = teams
                     .Where(x => string.IsNullOrWhiteSpace(request.Term)
                                 || x.Key.ToString().Contains(request.Term, StringComparison.InvariantCultureIgnoreCase)
                                 || x.Project.Name.Contains(request.Term, StringComparison.InvariantCultureIgnoreCase)
                                 || (x.Name?.Contains(request.Term, StringComparison.InvariantCultureIgnoreCase) ?? false))
                     .ToArray();


                foreach (var team in teams)
                {
                    var teamDto = _mapper.Map<TeamWithSlotListDto>(team);

                    teamDtos.Add(teamDto);
                }

            }

            return teamDtos;
        }
    }
}