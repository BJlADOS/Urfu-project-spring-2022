using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Expert.Project
{
    public class UpdateProjectDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Curator { get; set; }
        public string Organization { get; set; }
        public string Contacts { get; set; }
        public string Purpose { get; set; }
        public string Result { get; set; }
        public long[] CompetenciesIds { get; set; }
        public long LifeScenarioId { get; set; }
        public long KeyTechnologyId { get; set; }
        public bool IsPromoted { get; set; }
        public bool IsAvailable { get; set; }
        
        public int MaxTeamCount { get; set; }
    }
}
