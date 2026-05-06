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
        [Newtonsoft.Json.JsonConstructor]
        public Location(Coordinate coordinate, Address address)
        {
            _coordinate = coordinate ?? throw new ArgumentNullException(nameof(coordinate));
            _address = address ?? throw new ArgumentNullException(nameof(address));
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
