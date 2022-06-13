using System.Collections.Generic;
using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.KeyTechnology
{
    public class KeyTechnology : Entity<long>, IAggregateRoot, IKeyTechnology
    {
        public string Name { get; private set; }
        public ICollection<Project.Project> Projects { get; set; }
        public long EventId { get; set; }

        public KeyTechnology(string name, long eventId)
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