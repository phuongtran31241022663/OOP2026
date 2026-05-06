using System;
using System.Collections.Generic;

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
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            ValueObject other = (ValueObject)obj;
            IEnumerator<object> thisEnumerator = GetEqualityComponents().GetEnumerator();
            IEnumerator<object> otherEnumerator = other.GetEqualityComponents().GetEnumerator();

            while (true)
            {
                bool hasThis = thisEnumerator.MoveNext();
                bool hasOther = otherEnumerator.MoveNext();

                if (!hasThis && !hasOther)
                    return true;

                if (hasThis != hasOther)
                    return false;

                if (!object.Equals(thisEnumerator.Current, otherEnumerator.Current))
                    return false;
            }
        }

        public override int GetHashCode()
        {
            int hash = 0;

            foreach (object component in GetEqualityComponents())
            {
                hash = unchecked(hash * 31 + (component != null ? component.GetHashCode() : 0));
            }

            return hash;
        }
    }
}
