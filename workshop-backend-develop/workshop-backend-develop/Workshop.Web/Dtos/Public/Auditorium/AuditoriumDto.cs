using Workshop.Web.Dtos.Expert.User;
using Workshop.Web.Dtos.Public.TeamSlot;

namespace Workshop.Web.Dtos.Public.Auditorium
{
    public class AuditoriumDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public ExpertUserDto[] Experts { get; set; }
        public TeamSlotDto[] Slots { get; set; }
    }
}
