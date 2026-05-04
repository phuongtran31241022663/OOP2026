using System;
using System.Collections.Generic;

namespace Domain.SharedKernel
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity(Guid id) => Id = id;
        protected Entity() { } // for serialization

        protected internal void AddEvent(DomainEvent eventItem)
        {
            // No-op: domain events are not used in this project.
        }
        // cái này liên quan đến vấn đề event và domain event thì phải, giờ hiện cũng chả dùng
        public IReadOnlyList<DomainEvent> GetEvents() => Array.Empty<DomainEvent>();

        public void ClearEvents()
        {
            // No-op: domain events are not used in this project.
        }

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