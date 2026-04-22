using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.SharedKernel
{
    /// <summary>
    /// Base class for value objects. Value objects are immutable and have no identity.
    /// </summary>
    public abstract class ValueObject
    {
        // Yêu cầu lớp con liệt kê các thuộc tính để so sánh
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (!(obj is ValueObject)) return false;
            var other = (ValueObject)obj;

            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Aggregate(0, (hash, obj) => unchecked(hash * 31 + (obj != null ? obj.GetHashCode() : 0)));
        }
    }
}
