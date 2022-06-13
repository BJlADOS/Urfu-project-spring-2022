using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Workshop.Web.Dtos.Public.Project
{
    public class GetProjectsDto
    {
        public string Term { get; set; }
        public long? LifeScenarioId { get; set; }
        public long? KeyTechnologyId { get; set; }
        public string Curator { get; set; }
        public string Customer { get; set; }
    }
}
