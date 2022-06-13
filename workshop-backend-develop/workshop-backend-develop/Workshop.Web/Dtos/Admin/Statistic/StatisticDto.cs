using Workshop.Web.Dtos.Public.KeyTechnology;
using Workshop.Web.Dtos.Public.LifeScenario;

namespace Workshop.Web.Dtos.Admin.Statistic
{
    public class StatisticDto
    {
        public LifeScenarioDto LifeScenario { get; set; }
        public KeyTechnologyDto KeyTechnology { get; set; }
        public int StudentsCount { get; set; }
        public int TeamsCount { get; set; }
    }
}
