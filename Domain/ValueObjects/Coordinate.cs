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
        public Coordinate() { }
        public Coordinate(double lat, double lng)
        {
            _longitude = lng;
            _latitude = lat;
        }
        #endregion
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Longitude;
            yield return Latitude;
        }
    }
}
