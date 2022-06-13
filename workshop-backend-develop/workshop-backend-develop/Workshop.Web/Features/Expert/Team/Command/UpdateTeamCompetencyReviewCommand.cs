using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.TeamCompetencyReview;
using Workshop.Core.Domain.Model.TeamReview;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Expert.Team.Command
{
    public class UpdateTeamCompetenyReviewCommand : IRequest
    {
        public long TeamId { get; set; }
        public long CompetencyId { get; set; }
        public int Mark { get; set; }
    }

    public class UpdateTeamCompetenyReviewCommandHandler : AsyncRequestHandler<UpdateTeamCompetenyReviewCommand>
    {
        private TeamRepository _teamRepository;
        private CompetencyRepository _competencyRepository;
        private IUserProfileProvider _profileProvider;

        public UpdateTeamCompetenyReviewCommandHandler(TeamRepository teamRepository, CompetencyRepository competencyRepository, IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
            _competencyRepository = competencyRepository;
        }

        protected override async Task Handle(UpdateTeamCompetenyReviewCommand request, CancellationToken cancellationToken)
        {
            var expert = _profileProvider.GetProfile().User;
            var team = await _teamRepository.SingleAsync(TeamSpecification.GetById(request.TeamId, expert.EventId), cancellationToken);

            var competency = await _competencyRepository.SingleAsync(CompetencySpecification.GetById(request.CompetencyId), cancellationToken);

            if (competency == null || team == null)
            {
                throw new ArgumentException();
            }

            var prevReview = team.TeamCompetencyReviews.SingleOrDefault(r => r.ExpertId == expert.Key && r.CompetencyId == request.CompetencyId);

            if (prevReview == null)
            {
                team.TeamCompetencyReviews.Add(new TeamCompetencyReview(team, expert, competency, request.Mark));
            }
            else
            {
                if (prevReview.Mark == request.Mark)
                {
                    team.TeamCompetencyReviews.Remove(prevReview);
                }
                else
                {
                    prevReview.UpdateMark(request.Mark);
                }
            }

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
