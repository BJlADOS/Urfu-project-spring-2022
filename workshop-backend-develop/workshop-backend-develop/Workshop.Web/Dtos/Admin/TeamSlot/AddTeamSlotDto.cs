using System;

namespace Workshop.Web.Dtos.Admin.TeamSlot
{
    public class AddTeamSlotDto
    {
        public long AuditoriumId { get; set; }
        public DateTime Time { get; set; }
    }
}