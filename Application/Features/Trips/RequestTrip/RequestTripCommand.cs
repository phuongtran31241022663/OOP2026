using System;
using Domain.Enums;
using Domain.ValueObjects;

namespace Application.Features.Trips.RequestTrip
{
    public class RequestTripCommand
    {
        public Guid PassengerId { get; set; }
        public VehicleType VehicleType { get; set; }
        public Location Pickup { get; set; }
        public Location Destination { get; set; }
    }
}
