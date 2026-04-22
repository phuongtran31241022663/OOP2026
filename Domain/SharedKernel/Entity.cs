using System;
using System.Collections.Generic;

namespace Domain.SharedKernel
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity(Guid id)
        {
            Id = id;
        }
        private List<DomainEvent> _events = new List<DomainEvent>();

        public IReadOnlyCollection<DomainEvent> DomainEvents => _events;

        protected void AddEvent(DomainEvent e)
        {
            _events.Add(e);
        }

        public void ClearEvents()
        {
            _events.Clear();
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Entity other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
