using System.Collections.Generic;
using Workshop.Core.Domain.Model.Competency;
using Workshop.Core.Domain.Model.UserCompetency;

namespace Workshop.Web.Dtos.TeamLead
{
    public class GetUserDto
    {
        public string Term { get; set; }
        public ICollection<int> CompetenciesIds { get; set; }
    }
}
