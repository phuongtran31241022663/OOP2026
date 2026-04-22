using Application.DTOs;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITripService
    {
        event Action<TripDto> TripStatusChanged;

        TripDto RequestTrip(Guid passengerId, Location pickup, Location destination, VehicleType vehicleType);
        TripDto GetTripDto(Guid tripId);
        IEnumerable<TripDto> GetPendingTrips();
        IEnumerable<TripDto> GetTripsByPassenger(Guid passengerId);
        bool CanTripBeCancelled(Guid tripId);
        bool TryAssignDriver(Guid tripId, Guid driverId);
        void ArriveAtPickup(Guid tripId);
        void StartTrip(Guid tripId);
        void CompleteTrip(Guid tripId, double distance, double duration, Money fare);
        void CancelTrip(Guid tripId, string reason);
        Task<TripDto> GetActiveTripForDriverAsync(Guid driverId);
        Task<TripDto> GetActiveTripForPassengerAsync(Guid passengerId);
    }
}
