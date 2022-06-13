using System;
using Workshop.Core.Domain.Model.Team;
using Workshop.Web.Dtos.Public.TeamSlot;

namespace Workshop.Web.Dtos.Public.Team
{
    public class TeamListItemDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TeamStatus TeamStatus { get; set; }
        public DateTime? TeamCompleteDate { get; set; }
        public int UsersCount { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public int TeamCapacity { get; set; }
        public int? Mark { get; set; }
        public TeamSlotDto TeamSlot { get; set; }
    }
}
