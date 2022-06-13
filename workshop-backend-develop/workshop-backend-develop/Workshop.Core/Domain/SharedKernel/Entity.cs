using System;
using System.Collections.Generic;
using System.Text;

namespace Workshop.Core.Domain.SharedKernel
{
    public abstract class Entity<TKey> : Entity
    {
        protected Entity(TKey key)
        {
            Key = key;
        }

        protected Entity()
        {

        }

        public TKey Key { get; private set; }
    }

    public abstract class Entity
    {
        private List<IDomainEvent> _domainEvents;
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        public void AddEvent(IDomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
