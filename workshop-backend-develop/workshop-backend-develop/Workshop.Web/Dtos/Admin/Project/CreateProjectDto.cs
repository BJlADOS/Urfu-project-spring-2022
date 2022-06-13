using System.Collections.Generic;

namespace Workshop.Web.Dtos.Admin.Project
{
    public class CreateProjectDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Purpose { get; set; }
        public string Result { get; set; }
        public string Curator { get; set; }
        public string Organization { get; set; }
        public string Contacts { get; set; }
        public string LifeScenarioName { get; set; }
        public string KeyTechnologyName { get; set; }
        public List<string> HardSkills { get; set; }
        public List<string> SoftSkills { get; set; }
        public List<string> Roles { get; set; }
        public int TeamsSize { get; set; }
        public int TeamLimit { get; set; }
    }
}