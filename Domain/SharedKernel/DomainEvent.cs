using System;

namespace Domain.SharedKernel
{
 	public abstract class DomainEvent : IEquatable<DomainEvent>
 	{
        public Guid Id { get; }
        public DateTime OccurredOn { get; }

        protected DomainEvent()
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.UtcNow;
        }

        public bool Equals(DomainEvent other)
        {
            return other != null && Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DomainEvent);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
