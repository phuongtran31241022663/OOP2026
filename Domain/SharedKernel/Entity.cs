using System;
using System.Collections.Generic;

namespace Domain.SharedKernel
{
    public abstract class Entity
    {
        private readonly List<DomainEvent> _domainEvents = new List<DomainEvent>();

        public Guid Id { get; protected set; }

        protected Entity(Guid id) => Id = id;
        protected Entity() { } // for serialization

        protected internal void AddEvent(DomainEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public IReadOnlyList<DomainEvent> GetEvents() => _domainEvents.AsReadOnly();
        public void ClearEvents() => _domainEvents.Clear();
        public override bool Equals(object obj)
        {
            if (obj is not Entity other) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return Id.Equals(other.Id);
        }
        public override int GetHashCode() => Id.GetHashCode();
        public static bool operator ==(Entity a, Entity b) => (a is null && b is null) || (a is not null && a.Equals(b));
        public static bool operator !=(Entity a, Entity b) => !(a == b);
    }
}