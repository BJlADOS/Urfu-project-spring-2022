using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Expert.Team
{
    public class TeamReviewDto
    {
        public int? GoalsAndTasks { get; set; }
        public int? Solution { get; set; }
        public int? Impact { get; set; }
        public int? Presentation { get; set; }
        public int? Technical { get; set; }
        public int? Result { get; set; }
        public int? Knowledge { get; set; }
    }
}
