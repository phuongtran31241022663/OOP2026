using Application.Events;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ITripService
    {
        // Observer pattern event
        event EventHandler<TripStatusChangedEventArgs> TripStatusChanged;

        // Commands (async)
        Task<Trip> CreateTripAsync(Guid passengerId, Route route, Fare fare, VehicleType vehicleType);
        Task MatchDriverAsync(Guid tripId, Guid driverId);
        Task MarkAsArrivedAsync(Guid tripId);
        Task StartTripAsync(Guid tripId);
        Task CompleteTripAsync(Guid tripId);
        Task CancelTripAsync(Guid tripId, string reason);

        // Queries (async)
        Task<Trip> GetTripAsync(Guid tripId);
        Task<Trip> GetActiveTripForDriverAsync(Guid driverId);
        Task<Trip> GetActiveTripForPassengerAsync(Guid passengerId);
        Task<List<Trip>> GetPendingTripsAsync();
        Task<List<Trip>> GetTripsByDriverAsync(Guid driverId);
        Task<List<Trip>> GetTripsByPassengerAsync(Guid passengerId);
        Task<bool> CanTripBeCancelledAsync(Guid tripId);
    }
}