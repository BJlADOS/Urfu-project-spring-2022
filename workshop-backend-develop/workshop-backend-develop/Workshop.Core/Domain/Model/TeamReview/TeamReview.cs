using System;
using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.TeamReview
{
    public class TeamReview : ITeamReview, IAggregateRoot
    {
        public long TeamId { get; set; }
        public long ExpertId { get; set; }
        public virtual Team.Team Team { get; }
        public virtual User.User Expert { get; }
        public int? GoalsAndTasks { get; set; }
        public int? Solution { get; set; }
        public int? Impact { get; set; }
        public int? Presentation { get; set; }
        public int? Technical { get; set; }
        public int? Result { get; set; }
        public int? Knowledge { get; set; }

        private TeamReview()
        {
        }

        public TeamReview(Team.Team team, User.User expert)
        {
            Team = team;
            Expert = expert;
        }
    }
}