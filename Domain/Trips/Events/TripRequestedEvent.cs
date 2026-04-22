using System;
using Domain.SharedKernel;
using Domain.ValueObjects;
using Domain.Enums;

namespace Domain.Trips.Events
{
    public class TripRequestedEvent : DomainEvent
    {
        public Guid TripId { get; }
        public Guid PassengerId { get; }
        public Location Pickup { get; }
        public Location Destination { get; }
        public VehicleType VehicleType { get; } 

        public TripRequestedEvent(
            Guid tripId,
            Guid passengerId,
            Location pickup,
            Location destination,
            VehicleType vehicleType)
        {
            TripId = tripId;
            PassengerId = passengerId;
            Pickup = pickup;
            Destination = destination;
            VehicleType = vehicleType;
        }
    }
}
