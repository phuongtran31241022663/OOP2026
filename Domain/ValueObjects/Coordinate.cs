using System;
using System.Collections.Generic;
using Domain.SharedKernel;

namespace Domain.ValueObjects
{
    public sealed class Coordinate : ValueObject
    {
        #region Backing Fields
        private readonly double _latitude;
        private readonly double _longitude;
        #endregion
        #region Properties
        public double Longitude => _longitude;
        public double Latitude => _latitude;
        #endregion
        #region Constructors
        [Newtonsoft.Json.JsonConstructor]
        public Coordinate(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
                throw new ArgumentOutOfRangeException(nameof(latitude), "Vĩ độ phải nằm trong khoảng từ -90 đến 90.");
            if (longitude < -180 || longitude > 180)
                throw new ArgumentOutOfRangeException(nameof(longitude), "Kinh độ phải nằm trong khoảng từ -180 đến 180.");
            _longitude = longitude;
            _latitude = latitude;
        }

        #endregion
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Longitude;
            yield return Latitude;
        }
    }
}
