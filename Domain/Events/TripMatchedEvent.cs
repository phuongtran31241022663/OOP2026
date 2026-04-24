using System;
using Domain.SharedKernel;

namespace Domain.Events
{
    public class TripMatchedEvent : DomainEvent
    {
        public Guid TripId { get; }
        public Guid DriverId { get; }
        public TripMatchedEvent(Guid tripId, Guid driverId)
        {
            TripId = tripId;
            DriverId = driverId;
        }
    }
}
