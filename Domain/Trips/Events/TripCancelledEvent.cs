using System;
using Domain.SharedKernel;

namespace Domain.Trips.Events
{
    public class TripCancelledEvent : DomainEvent
    {
        public Guid TripId { get; }
        public string Reason { get; }

        public TripCancelledEvent(Guid tripId, string reason)
        {
            TripId = tripId;
            Reason = reason;
        }
    }
}
