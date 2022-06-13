using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Expert.Team
{
    public class TeamWithSlotListDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int UsersCount { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int TeamCapacity { get; set; }
        public int? Mark { get; set; }
        public long AuditoriumId { get; set; }
        public string AuditoriumName { get; set; }
        public DateTime SlotTime { get; set; }
    }
}
