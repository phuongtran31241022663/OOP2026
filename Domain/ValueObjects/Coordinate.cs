﻿using System;
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
            if (lat < -90 || lat > 90)
                throw new ArgumentOutOfRangeException(nameof(lat), "Vĩ độ phải nằm trong khoảng từ -90 đến 90.");
            if (lng < -180 || lng > 180)
                throw new ArgumentOutOfRangeException(nameof(lng), "Kinh độ phải nằm trong khoảng từ -180 đến 180.");
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
