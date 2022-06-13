using System;
using System.Collections.Generic;
using System.Text;
using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.TeamCompetencyReview
{
    public class TeamCompetencyReview : ITeamCompetencyReview, IAggregateRoot
    {
        public long TeamId { get; set; }
        public long ExpertId { get; set; }
        public long CompetencyId { get; set; }
        public virtual Team.Team Team { get; }
        public virtual User.User Expert { get; }
        public virtual Competency.Competency Competency { get; }
        public int Mark { get; private set; }

        private TeamCompetencyReview()
        {
        }

        public TeamCompetencyReview(Team.Team team, User.User expert, Competency.Competency competency, int mark)
        {
            Team = team;
            Expert = expert;
            Competency = competency;
            Mark = mark;
        }

        public void UpdateMark(int mark)
        {
            Mark = mark;
        }
    }
}