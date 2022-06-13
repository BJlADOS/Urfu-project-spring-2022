using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Workshop.Core.Domain.Model.Team;
using Workshop.Core.Domain.SharedKernel.Repository;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Helpers;
using Workshop.Core.Services.Security;
using Workshop.Web.Dtos.Expert.Team;
using Workshop.Web.Dtos.Public.Team;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Expert.Team.Query
{
    public class ExpertTeamGetQuery : IRequest<TeamDto>
    {
        public long TeamId { get; set; }
    }

    public class ExpertTeamGetQueryHandler : IRequestHandler<ExpertTeamGetQuery, TeamDto>
    {
        private ITeamRepository _teamRepository;
        private IMapper _mapper;
        private IUserProfileProvider _profileProvider;

        public ExpertTeamGetQueryHandler(ITeamRepository teamRepository, IMapper mapper, IUserProfileProvider profileProvider)
        {
            _mapper = mapper;
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
        }

        public async Task<TeamDto> Handle(ExpertTeamGetQuery request, CancellationToken cancellationToken)
        {
            var expert = _profileProvider.GetProfile().User;
            var team = await _teamRepository.SingleOrDefaultAsync(TeamSpecification.GetById(request.TeamId, expert.EventId), cancellationToken);
            if (team == null)
                throw new NotFoundException();

            if (team.TeamStatus == TeamStatus.TestWork && team.TeamCompleteDate < DateTimeHelper.GetCurrentTime())
            {
                team.CompleteTest();
                await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            }

            var teamDto = _mapper.Map<TeamDto>(team);
            teamDto.Review = _mapper.Map<TeamReviewDto>(team.TeamReviews.SingleOrDefault(r => r.ExpertId == expert.Key));
            teamDto.CompetencyReview = _mapper.Map<TeamCompetencyReviewDto[]>(team.TeamCompetencyReviews.Where(r => r.ExpertId == expert.Key).ToArray());

            return teamDto;
        }
    }
}
