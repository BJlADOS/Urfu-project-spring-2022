using System.Collections.Generic;
using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.LifeScenario
{
    public class LifeScenario : Entity<long>, IAggregateRoot, ILifeScenario
    {
        public string Name { get; private set; }
        public ICollection<Project.Project> Projects { get; set; }
        public long EventId { get; set; }

        private LifeScenario()
        {
        }

        public LifeScenario(string name, long eventId)
        {
            Name = name;
            Projects = new List<Project.Project>();
            EventId = eventId;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}