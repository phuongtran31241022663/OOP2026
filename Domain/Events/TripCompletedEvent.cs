using System;
using Domain.ValueObjects;
using Domain.SharedKernel;

namespace Domain.Events
{
    public class TripCompletedEvent : DomainEvent
    {
        public Guid TripId { get; }
        public Guid PassengerId { get; }
        public Guid DriverId { get; }
        public Fare Fare { get; }

        public TripCompletedEvent(Guid tripId, Guid passengerId, Guid driverId, Fare fare)
        {
            TripId = tripId;
            PassengerId = passengerId;
            DriverId = driverId;
            Fare = fare;
        }
    }
}
