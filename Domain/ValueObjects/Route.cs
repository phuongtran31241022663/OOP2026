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
        #region Fields
        private readonly Location _pickup;
        private readonly Location _destination;
        private readonly double _distance; // Km
        private readonly TimeSpan _duration;
        private readonly string _polyline;
        #endregion
        #region Properties
        public Location Pickup => _pickup;
        public Location Destination => _destination;
        public double Distance => _distance;
        public TimeSpan Duration => _duration;
        public string Polyline => _polyline;
        #endregion
        #region Constructors
        private Route() { }
       public Route(Location pickup, Location destination, double distance, TimeSpan duration, string polyline)
        {
            if (pickup == null) throw new ArgumentNullException(nameof(pickup));
            if (destination == null) throw new ArgumentNullException(nameof(destination));
            if (distance <= 0) throw new ArgumentOutOfRangeException(nameof(distance), "Khoảng cách phải lớn hơn 0.");
            if (duration.TotalSeconds < 0) throw new ArgumentOutOfRangeException(nameof(duration), "Thời gian phải lớn hơn hoặc bằng 0.");

            _pickup = pickup;
            _destination = destination;
            _distance = distance;
            _duration = duration;
            _polyline = polyline ?? string.Empty;
        }
        #endregion
        protected override IEnumerable<object> GetEqualityComponents()
        {
          yield return Pickup;
            yield return Destination;
            yield return Distance;
            yield return Duration;
            yield return Polyline;
        }
    }
}
