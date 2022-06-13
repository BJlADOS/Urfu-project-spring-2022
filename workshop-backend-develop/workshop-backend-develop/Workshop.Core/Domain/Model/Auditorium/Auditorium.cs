using System.Collections.Generic;
using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.Auditorium
{
    public class Auditorium : Entity<long>, IAggregateRoot, IAuditorium
    {
        public string Name { get; private set; }
        public int Capacity { get; private set; }

        //Аудитория по умаолчанию, в нее будут скидываться команды, если все аудитории заняты
        public bool IsDefaultAuditory { get; private set; }
        public long EventId { get; private set; }
        public virtual ICollection<UserAuditorium.UserAuditorium> Experts { get; private set; }
        public List<TeamSlot.TeamSlot> TeamSlots { get; private set; }

        private Auditorium()
        {
        }

        public Auditorium(long eventId, string name, int capacity, bool isDefaultAuditory)
        {
            EventId = eventId;
            Capacity = capacity;
            Name = name;
            IsDefaultAuditory = isDefaultAuditory;
            TeamSlots = new List<TeamSlot.TeamSlot>();
            Experts = new List<UserAuditorium.UserAuditorium>();
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateCapacity(int capacity)
        {
            Capacity = capacity;
        }

        public void UpdateDefaultAuditory(bool isDefault)
        {
            IsDefaultAuditory = isDefault;
        }
    }
}