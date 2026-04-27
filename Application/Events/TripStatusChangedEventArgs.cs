using System;

namespace Application.Events
{
    public class TripStatusChangedEventArgs : EventArgs
    {
        public Guid TripId { get; }
        public string NewStatus { get; }
        public Guid? DriverId { get; }

        public TripStatusChangedEventArgs(Guid tripId, string newStatus, Guid? driverId)
        {
            TripId = tripId;
            NewStatus = newStatus;
            DriverId = driverId;
        }
    }
}
