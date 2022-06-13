using Workshop.Core.Domain.SharedKernel;

namespace Workshop.Core.Domain.Model.Event
{
    public class Event : Entity<long>, IAggregateRoot, IEvent
    {
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public Event()
        {
        }

        public Event(string name)
        {
            Name = name;
            IsActive = true;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }

        public void UpdateActivation(bool isActive)
        {
            IsActive = isActive;
        }
    }
}