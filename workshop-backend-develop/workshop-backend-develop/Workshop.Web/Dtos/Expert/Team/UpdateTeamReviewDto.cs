using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Expert.Team
{
    public class UpdateTeamReviewDto
    {
        public long TeamId { get; set; }
        public TeamReviewDto Marks { get; set; }
    }
}
