using System;
using Workshop.Web.Dtos.Public.Team;

namespace Workshop.Web.Dtos.Public.TeamSlot
{
    public class TeamSlotDto
    {
        public long Id { get; set; }
        public long AuditoriumId { get; set; }
        public DateTime Time { get; set; }
        public ShortTeamDto Team { get; set; }
    }
}