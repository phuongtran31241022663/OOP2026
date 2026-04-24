using Application.DTOs;
using Domain.Enums;
using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface ITripService
    {
        event Action<TripDto> TripStatusChanged;

        // Command operations (sync)
        TripDto RequestTrip(Guid passengerId, Location pickup, Location destination, VehicleType vehicleType);
        bool TryAssignDriver(Guid tripId, Guid driverId);
        void ArriveAtPickup(Guid tripId);
        void StartTrip(Guid tripId);
        void CompleteTrip(Guid tripId, decimal fareAmount);
        void CancelTrip(Guid tripId, string reason);

        // Query operations (sync)
        Trip GetTrip(Guid tripId);
        Trip GetActiveTripForDriver(Guid driverId);
        Trip GetActiveTripForPassenger(Guid passengerId);
        List<Trip> GetPendingTrips();
        List<Trip> GetTripsByDriver(Guid driverId);
        List<Trip> GetTripsByPassenger(Guid passengerId);
        TripDto GetTripDto(Guid tripId);
        bool CanTripBeCancelled(Guid tripId);
    }
}
