using System;
using System.Collections.Generic;
using Domain.SharedKernel;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Value Object để quản lý các số liệu của chuyến đi (khoảng cách và thời gian).
    /// </summary>
    public sealed class Route : ValueObject
    {
        private readonly double _distance; // Km
        private readonly TimeSpan _duration;
        private readonly Location _pickup;
        private readonly Location _destination;
        private readonly string _encodedPolyline;
        public double Distance => _distance;
        public TimeSpan Duration => _duration;

        public Location Pickup
        {
            get { return _pickup; }
        }

        public Location Destination
        {
            get { return _destination; }
        }
        public string EncodedPolyline
        {
            get { return _encodedPolyline; }
        }
        private Route() { }
        public Route(Location pickup, Location des, double distance, TimeSpan duration, string encodedPolyline = null)
        {
            if (distance < 0)
                throw new ArgumentOutOfRangeException(nameof(distance), "Khoảng cách không được âm.");

            if (duration.TotalSeconds < 0)
                throw new ArgumentOutOfRangeException(nameof(duration), "Thời gian không được âm.");

            _distance = distance;
            _duration = duration;
            _pickup = pickup ?? throw new ArgumentNullException(nameof(pickup));
            _destination = des ?? throw new ArgumentNullException(nameof(des));
            _encodedPolyline = encodedPolyline ?? string.Empty;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return _distance;
            yield return _duration;
            yield return _pickup;
            yield return _destination;
            yield return _encodedPolyline;
        }
    }
}
