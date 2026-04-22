using System;
using Domain.SharedKernel;

namespace Domain.Trips.Events
{
    public class TripArrivedEvent : DomainEvent
    {
        public Guid TripId { get; }

        public TripArrivedEvent(Guid tripId)
        {
            TripId = tripId;
        }
    }
}
