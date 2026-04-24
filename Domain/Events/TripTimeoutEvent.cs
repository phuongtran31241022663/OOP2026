using System;
using Domain.SharedKernel;

namespace Domain.Events
{
    public class TripTimeoutEvent : DomainEvent
    {
        public Guid TripId { get; }

        public TripTimeoutEvent(Guid tripId)
        {
            TripId = tripId;
        }
    }
}
