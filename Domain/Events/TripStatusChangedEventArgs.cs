using System;
using Domain.Enums;

namespace Application.Events
{
    public class TripStatusChangedEventArgs : EventArgs
    {
        public Guid TripId { get; }
        public TripStatus NewStatus { get; }
        public Guid? DriverId { get; }

        public TripStatusChangedEventArgs(Guid tripId, TripStatus newStatus, Guid? driverId)
        {
            TripId = tripId;
            NewStatus = newStatus;
            DriverId = driverId;
        }
    }
}