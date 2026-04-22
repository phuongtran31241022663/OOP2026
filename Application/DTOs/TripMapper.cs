using Domain.Trips;
using Domain.Users;
using Domain.Users.Drivers;
using Domain.Users.Passengers;
using Domain.ValueObjects;
using System;

namespace Application.DTOs
{
    public static class TripMapper
    {
        public static TripDto ToDto(this Trip trip, IUserRepository userRepo)
        {
            var passenger = userRepo.GetById(trip.PassengerId) as Passenger;
            var driver = trip.DriverId.HasValue ? userRepo.GetById(trip.DriverId.Value) as Driver : null;

            return new TripDto
            {
                Id = trip.Id,
                PassengerId = trip.PassengerId,
                DriverId = trip.DriverId,
                PassengerName = passenger?.Name ?? "Unknown Passenger",
                DriverName = driver?.Name ?? "Unknown Driver",
                Pickup = trip.Route.Pickup,
                Destination = trip.Route.Destination,
                Status = trip.Status,
                Fare = trip.Fare?.TotalAmount,
                RequestedAt = trip.RequestAt,
                Distance = trip.Route.Distance,
                Duration = trip.Route.Duration.TotalSeconds,
                VehicleType = trip.VehicleType,
                Version = trip.Version,
                IsPaid = trip.IsPaid,
                IsRated = trip.IsRated
            };
        }
    }
}