using System;

namespace Workshop.Web.Dtos.Admin.TeamSlot
{
    public class AddTeamSlotsDto
    {
        public long AuditoriumId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
    }
}