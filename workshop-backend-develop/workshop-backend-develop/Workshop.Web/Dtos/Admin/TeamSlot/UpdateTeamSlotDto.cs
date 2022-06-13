using System;

namespace Workshop.Web.Dtos.Admin.TeamSlot
{
    public class UpdateTeamSlotDto
    {
        public long Id { get; set; }
        public long? TeamId { get; set; }
        public long AuditorumId { get; set; }
        public DateTime Time { get; set; }
    }
}