using System;
using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.TeamSlot
{
    public class TeamSlot : Entity<long>, IAggregateRoot
    {
        public long AuditoriumId { get; set; }
        public virtual Auditorium.Auditorium Auditorium { get; set; }
        public long? TeamId { get; set; }
        public virtual Team.Team Team { get; private set; }
        public DateTime Time { get; set; }

        public TeamSlot(long auditoriumId, DateTime time)
        {
            AuditoriumId = auditoriumId;
            Time = time;
        }

        public void SignTeam(Team.Team team)
        {
            Team = team;
        }
    }
}