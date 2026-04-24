using Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Value Object representing a geographic location.
    /// Bất biến (immutable) và so sánh theo giá trị (value-based equality).
    /// </summary>
    public sealed class Location : ValueObject
    {
        #region Fields
        private readonly Coordinate _coordinate;
        private readonly Address _address;
        #endregion
        #region Properties
        public Coordinate Coordinate => _coordinate;

        public Address Address => _address;
        #endregion

        #region Constructors
        // Constructor cho ORM (như EF Core) - giữ private để đảm bảo tính đóng gói
        public Location() { }

        public Location(Coordinate coordinate, Address address)
        {
            _coordinate = coordinate;
            _address = address;
        }

        #endregion

        #region ValueObject Equality
        protected override IEnumerable<object> GetEqualityComponents()
        {
            // Phải bao gồm cả tọa độ và địa chỉ để xác định tính duy nhất của giá trị
            yield return Coordinate;
            yield return Address;
        }
        #endregion
    }
}