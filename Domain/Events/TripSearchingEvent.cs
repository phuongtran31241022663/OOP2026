using System;
using Domain.SharedKernel;

namespace Domain.Events
{
    public class TripSearchingEvent : DomainEvent
    {
        public Guid TripId { get; }
        public int AttemptNumber { get; }

        public TripSearchingEvent(Guid tripId, int attemptNumber = 1)
        {
            TripId = tripId;
            AttemptNumber = attemptNumber;
        }
    }
}
