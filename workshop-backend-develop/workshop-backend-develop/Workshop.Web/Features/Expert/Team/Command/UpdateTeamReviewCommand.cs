using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workshop.Core.Domain.Model.TeamReview;
using Workshop.Core.Domain.Specification;
using Workshop.Core.Services.Security;
using Workshop.Infrastructure.Storage.Repositories;

namespace Workshop.Web.Features.Expert.Team.Command
{
    public class UpdateTeamReviewCommand : IRequest
    {
        public long TeamId { get; set; }
        public int? GoalsAndTasks { get; set; }
        public int? Solution { get; set; }
        public int? Impact { get; set; }
        public int? Presentation { get; set; }
        public int? Technical { get; set; }
        public int? Result { get; set; }
        public int? Knowledge { get; set; }
    }

    public class UpdateTeamReviewCommandHandler : AsyncRequestHandler<UpdateTeamReviewCommand>
    {
        private TeamRepository _teamRepository;
        private IUserProfileProvider _profileProvider;

        public UpdateTeamReviewCommandHandler(TeamRepository teamRepository, IUserProfileProvider profileProvider)
        {
            _teamRepository = teamRepository;
            _profileProvider = profileProvider;
        }

        protected override async Task Handle(UpdateTeamReviewCommand request, CancellationToken cancellationToken)
        {
            var expert = _profileProvider.GetProfile().User;
            var team = await _teamRepository.SingleAsync(TeamSpecification.GetById(request.TeamId, expert.EventId), cancellationToken);

            var prevReview = team.TeamReviews.SingleOrDefault(r => r.ExpertId == expert.Key);

            if (prevReview == null)
            {
                var newReview = new TeamReview(team, expert);
                team.TeamReviews.Add(newReview);
                prevReview = newReview;
            }
            prevReview.GoalsAndTasks = request.GoalsAndTasks;
            prevReview.Solution = request.Solution;
            prevReview.Impact = request.Impact;
            prevReview.Presentation = request.Presentation;
            prevReview.Technical = request.Technical;
            prevReview.Result = request.Result;
            prevReview.Knowledge = request.Knowledge;

            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
