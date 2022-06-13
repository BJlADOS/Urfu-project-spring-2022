using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Workshop.Core.Domain.Model.TeamCompetencyReview;
using Workshop.Core.Domain.Model.TeamReview;
using Workshop.Core.Domain.Model.User;
using Workshop.Core.Domain.Model.UserAuditorium;
using Workshop.Core.Domain.Specification;
using Workshop.Infrastructure.Storage.Repositories;
using Workshop.Web.Exceptions;

namespace Workshop.Web.Features.Public.ExpertUser.Command
{
    public class ChangeAuditoriumCommand : IRequest
    {
        public long CurrentExpertId { get; set; }
        public long NewExpertId { get; set; }
        public long AuditoriumId { get; set; }
    }

    public class ChangeAuditoriumCommandHandler : AsyncRequestHandler<ChangeAuditoriumCommand>
    {
        private readonly CompetencyRepository _competencyRepository;
        private readonly TeamRepository _teamRepository;
        private readonly UserRepository _userRepository;

        public ChangeAuditoriumCommandHandler(TeamRepository teamRepository,
            UserRepository userRepository,
            CompetencyRepository competencyRepository)
        {
            _teamRepository = teamRepository;
            _userRepository = userRepository;
            _competencyRepository = competencyRepository;
        }

        protected override async Task Handle(ChangeAuditoriumCommand request,
            CancellationToken cancellationToken)
        {
            var currentExpert = await _userRepository.SingleAsync(
                UserSpecification.GetById(request.CurrentExpertId),
                cancellationToken);

            var newExpert = await _userRepository.SingleAsync(
                UserSpecification.GetById(request.NewExpertId),
                cancellationToken);

            if (currentExpert.UserType != UserType.Expert
                || newExpert.UserType != UserType.Expert)
                throw new ForbiddenException();

            var teams = await _teamRepository.TeamsWithReviewListAsync(
                TeamSpecification.GetTeamsByAuditoriumId(currentExpert.EventId, request.AuditoriumId),
                cancellationToken);

            foreach (var team in teams)
                await UpdateTeamReviews(team,
                    request.CurrentExpertId,
                    newExpert,
                    cancellationToken);

            var currentUserAuditorium = currentExpert.Auditoriums
                .FirstOrDefault(auditorium => auditorium.AuditoriumId == request.AuditoriumId);

            var newUserAuditorium = new UserAuditorium
            {
                AuditoriumId = request.AuditoriumId,
                UserId = request.NewExpertId
            };

            currentExpert.Auditoriums.Remove(currentUserAuditorium);
            newExpert.Auditoriums.Add(newUserAuditorium);

            await _userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            await _teamRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task UpdateTeamReviews(Core.Domain.Model.Team.Team team,
            long currentExpertId,
            Core.Domain.Model.User.User newExpert,
            CancellationToken cancellationToken)
        {
            var currentReview = team.TeamReviews
                .SingleOrDefault(review => review.ExpertId == currentExpertId);

            if (currentReview != null)
            {
                var newReview = new TeamReview(team, newExpert)
                {
                    GoalsAndTasks = currentReview.GoalsAndTasks,
                    Solution = currentReview.Solution,
                    Impact = currentReview.Impact,
                    Presentation = currentReview.Presentation,
                    Technical = currentReview.Technical,
                    Result = currentReview.Result,
                    Knowledge = currentReview.Knowledge
                };

                team.TeamReviews.Remove(currentReview);
                team.TeamReviews.Add(newReview);
            }

            var currentCompetencyReviews = team
                .TeamCompetencyReviews
                .Where(competencyReview => competencyReview.ExpertId == currentExpertId)
                .ToList();

            foreach (var competencyReview in currentCompetencyReviews)
            {
                var competency = await _competencyRepository.SingleAsync(
                    CompetencySpecification.GetById(competencyReview.CompetencyId),
                    cancellationToken);

                team.TeamCompetencyReviews.Remove(competencyReview);
                team.TeamCompetencyReviews.Add(new TeamCompetencyReview(
                    team,
                    newExpert,
                    competency,
                    competencyReview.Mark));
            }
        }
    }
}