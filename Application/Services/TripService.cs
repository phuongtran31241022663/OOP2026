using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Domain.Trips;
using Domain.ValueObjects;
using Domain.Users.Drivers;
using Domain.Users.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TripService : ITripService
    {
        public event Action<TripDto> TripStatusChanged;

        private readonly ITripRepository _tripRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly ISimulationService _simulationService;
        private readonly IFareRuleService _fareRuleService;

        public TripService(
            ITripRepository tripRepository,
            IDriverRepository driverRepository,
            IPassengerRepository passengerRepository,
            ISimulationService simulationService,
            IFareRuleService fareRuleService)
        {
            _tripRepository = tripRepository;
            _driverRepository = driverRepository;
            _passengerRepository = passengerRepository;
            _simulationService = simulationService;
            _fareRuleService = fareRuleService;
        }

        public TripDto RequestTrip(Guid passengerId, Location pickup, Location destination, VehicleType vehicleType)
        {
            if (pickup == null || destination == null)
                throw new ArgumentException("Pickup and destination are required");

            var passenger = _passengerRepository.GetById(passengerId);
            if (passenger == null || !passenger.IsActive)
                throw new Exception("Invalid passenger");

            var route = new Route(pickup, destination, 0, TimeSpan.Zero);
            var trip = new Trip(passengerId, vehicleType, route);
            trip.SetSearching();

            _tripRepository.Add(trip);
            TripStatusChanged?.Invoke(ToDto(trip));
            return ToDto(trip);
        }

        public bool TryAssignDriver(Guid tripId, Guid driverId)
        {
            var trip = _tripRepository.GetById(tripId);
            var driver = _driverRepository.GetById(driverId);

            if (trip == null || driver == null)
                throw new Exception("Trip or Driver not found");

            if (trip.DriverId.HasValue)
                throw new Exception("Already assigned");

            trip.MatchDriver(driverId);
            driver.SetOnTrip();

            _tripRepository.Update(trip);
            _driverRepository.Update(driver);

            TripStatusChanged?.Invoke(ToDto(trip));
            _simulationService.StartTripSimulation(trip.Id);
            return true;
        }

        public void StartTrip(Guid tripId)
        {
            var trip = _tripRepository.GetById(tripId);
            if (trip == null) return;

            trip.StartTrip();
            _tripRepository.Update(trip);
            TripStatusChanged?.Invoke(ToDto(trip));
        }

        public void ArriveAtPickup(Guid tripId)
        {
            var trip = _tripRepository.GetById(tripId);
            if (trip == null) return;

            trip.MarkAsArrived();
            _tripRepository.Update(trip);
            TripStatusChanged?.Invoke(ToDto(trip));
        }

        public void CompleteTrip(Guid tripId, decimal fareAmount)
        {
            var trip = _tripRepository.GetById(tripId);
            if (trip == null) throw new Exception("Trip not found");

            var fare = new Money(fareAmount);
            trip.CompleteTrip(fare);

            // Update driver and passenger stats
            if (trip.DriverId.HasValue)
            {
                var driver = _driverRepository.GetById(trip.DriverId.Value);
                if (driver != null)
                {
                    driver.SetAvailable();
                    driver.AddTrip();
                    _driverRepository.Update(driver);
                    _driverRepository.SaveChangesAsync().GetAwaiter().GetResult();
                }
            }

            var passenger = _passengerRepository.GetById(trip.PassengerId);
            if (passenger != null)
            {
                passenger.AddTrip();
                _passengerRepository.Update(passenger);
                _passengerRepository.SaveChangesAsync().GetAwaiter().GetResult();
            }

            _tripRepository.Update(trip);
            _tripRepository.SaveChangesAsync().GetAwaiter().GetResult();

            TripStatusChanged?.Invoke(ToDto(trip));
        }

        public void CancelTrip(Guid tripId, string reason)
        {
            var trip = _tripRepository.GetById(tripId);
            if (trip == null) throw new Exception("Trip not found");

            trip.Cancel(reason);

            if (trip.DriverId.HasValue)
            {
                var driver = _driverRepository.GetById(trip.DriverId.Value);
                if (driver != null)
                {
                    driver.SetAvailable();
                    _driverRepository.Update(driver);
                }
            }

            _tripRepository.Update(trip);
            TripStatusChanged?.Invoke(ToDto(trip));
        }

        public TripDto GetTripDto(Guid tripId)
        {
            var trip = _tripRepository.GetById(tripId);
            return ToDto(trip);
        }

        public IEnumerable<TripDto> GetPendingTrips()
        {
            var trips = _tripRepository.GetPendingTrips();
            return trips.Select(t => ToDto(t));
        }

        public IEnumerable<TripDto> GetTripsByPassenger(Guid passengerId)
        {
            var trips = _tripRepository.GetTripsByPassengerId(passengerId);
            return trips.Select(t => ToDto(t));
        }

        public Task<TripDto> GetActiveTripForDriverAsync(Guid driverId)
        {
            var trip = _tripRepository.GetTripsByDriverId(driverId)
                .Where(t => t.Status != TripStatus.Completed && t.Status != TripStatus.Cancelled && t.Status != TripStatus.Timeout)
                .OrderByDescending(t => t.RequestAt)
                .FirstOrDefault();

            return Task.FromResult(trip != null ? ToDto(trip) : null);
        }

        public Task<TripDto> GetActiveTripForPassengerAsync(Guid passengerId)
        {
            var trip = _tripRepository.GetTripsByPassengerId(passengerId)
                .Where(t => t.Status != TripStatus.Completed && t.Status != TripStatus.Cancelled && t.Status != TripStatus.Timeout)
                .OrderByDescending(t => t.RequestAt)
                .FirstOrDefault();

            return Task.FromResult(trip != null ? ToDto(trip) : null);
        }

        public bool CanTripBeCancelled(Guid tripId)
        {
            var trip = _tripRepository.GetById(tripId);
            return trip != null && (trip.Status == TripStatus.Searching || trip.Status == TripStatus.Matched);
        }

        private TripDto ToDto(Trip trip)
        {
            if (trip == null) return null;

            var passenger = _passengerRepository.GetById(trip.PassengerId);
            var driver = trip.DriverId.HasValue ? _driverRepository.GetById(trip.DriverId.Value) : null;

            return new TripDto
            {
                Id = trip.Id,
                PassengerId = trip.PassengerId,
                DriverId = trip.DriverId,
                PassengerName = passenger?.Name ?? "Unknown Passenger",
                DriverName = driver?.Name ?? "Unknown Driver",
                Pickup = trip.Route?.Pickup,
                Destination = trip.Route?.Destination,
                Status = trip.Status,
                Fare = trip.Fare?.TotalAmount,
                RequestedAt = trip.RequestAt,
                Distance = trip.Distance,
                Duration = trip.Duration,
                VehicleType = trip.VehicleType,
                Version = trip.Version,
                IsPaid = trip.IsPaid,
                IsRated = trip.IsRated
            };
        }
    }
}
