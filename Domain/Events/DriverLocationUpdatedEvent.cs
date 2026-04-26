using Domain.SharedKernel;
using Domain.ValueObjects;
using System;

namespace Domain.Events
{
    public class DriverLocationUpdatedEvent : DomainEvent
    {
        public Guid DriverId { get; }
        public Location NewLocation { get; }

        public DriverLocationUpdatedEvent(Guid driverId, Location newLocation)
        {
            DriverId = driverId;
            NewLocation = newLocation;
        }
    }
}