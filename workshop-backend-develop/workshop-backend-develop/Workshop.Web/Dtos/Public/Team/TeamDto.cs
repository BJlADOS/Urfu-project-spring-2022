using System;
using System.Collections.Generic;
using Workshop.Core.Domain.Model.Team;
using Workshop.Web.Dtos.Expert.Team;
using Workshop.Web.Dtos.Expert.User;
using Workshop.Web.Dtos.Public.Auditorium;
using Workshop.Web.Dtos.Public.Project;
using Workshop.Web.Dtos.Public.TeamSlot;
using Workshop.Web.Dtos.Public.User;

namespace Workshop.Web.Dtos.Public.Team
{
    public class TeamDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public TeamStatus TeamStatus { get; set; }
        public TeamSlotDto TeamSlot { get; set; }
        public ExpertUserDto Expert { get; set; }
        public DateTime? TeamCompleteDate { get; set; }
        public ShortProjectDto Project { get; set; }
        public TeamReviewDto Review { get; set; }
        public TeamCompetencyReviewDto[] CompetencyReview { get; set; }
        public ICollection<UserDto> Users { get; set; }
        public int? Mark { get; set; }
        public string Comment { get; set; }
        public string TestLink { get; set; }

        public bool IsEntried { get; set; }
        
        public string PMSLink { get; set; }
        public string RepositoryLink { get; set; }
        public string AdditionalLink { get; set; }
        
    }
}
