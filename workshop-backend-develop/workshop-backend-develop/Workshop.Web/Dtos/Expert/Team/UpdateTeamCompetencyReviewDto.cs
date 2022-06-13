using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Expert.Team
{
    public class UpdateTeamCompetencyReviewDto
    {
        public long TeamId { get; set; }
        public long CompetencyId { get; set; }
        public int Mark { get; set; }
    }
}
