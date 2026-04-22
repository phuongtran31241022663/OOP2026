using System;
using Domain.SharedKernel;

namespace Domain.Trips.Events
{
    public class TripStartedEvent : DomainEvent
    {
        public Guid TripId { get; }

        public TripStartedEvent(Guid tripId)
        {
            TripId = tripId;
        }
    }
}
