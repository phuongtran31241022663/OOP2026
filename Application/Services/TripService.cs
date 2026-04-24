using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Domain.FareRules;
using Domain.SharedKernel;
using Domain.Trips;
using Domain.Users.Drivers;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Application.Services
{
    public class TripService : ITripService
    {
        public event Action<TripDto> TripStatusChanged;

        private readonly ITripRepository _tripRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly ISimulationService _simulationService;
        private readonly IFareService _fareRuleService;
        private readonly IRepository<Trip> _tripRepo;
        private readonly IRepository<Driver> _driverRepo;
        private readonly IRepository<FareRule> _fareRuleRepo;
        public TripService(
            ITripRepository tripRepository,
            IDriverRepository driverRepository,
            IPassengerRepository passengerRepository,
            ISimulationService simulationService,
            IFareService fareRuleService)
        {
            _tripRepository = tripRepository;
            _driverRepository = driverRepository;
            _passengerRepository = passengerRepository;
            _simulationService = simulationService;
            _fareRuleService = fareRuleService;
        }
        public async Task<Trip> CreateTripAsync(Guid passengerId, Location pickup, Location destination, VehicleType vehicleType)
        {
            // 1. Tính khoảng cách (giả lập, thực tế gọi Google Maps API)
            double distance = CalculateDistance(pickup, destination);
            TimeSpan duration = TimeSpan.FromMinutes(distance * 2); // ước lượng

            var route = new Route(pickup, destination, distance, duration, "");

            // 2. Lấy FareRule để tính giá
            var rules = await _fareRuleRepo.GetAllAsync();
            var rule = rules.FirstOrDefault(r => r.VehicleType == vehicleType)
                ?? throw new Exception("Chưa có quy tắc giá cho loại xe này.");

            var fare = rule.CalculateFare(distance);

            // 3. Tạo Trip
            var trip = new Trip(passengerId, route, fare, vehicleType);

            // 4. Lưu
            _tripRepo.Add(trip);
            await _tripRepo.SaveChangesAsync();

            // 5. Trả về cho client
            return trip;
        }

        // Xác nhận tài xế
        public async Task MatchDriverAsync(Guid tripId, Guid driverId)
        {
            var trip = await _tripRepo.GetByIdAsync(tripId) ?? throw new Exception("Không tìm thấy chuyến.");
            var driver = await _driverRepo.GetByIdAsync(driverId) ?? throw new Exception("Không tìm thấy tài xế.");

            trip.MatchDriver(driverId);
            driver.SetOnTrip();

            _tripRepo.Update(trip);
            _driverRepo.Update(driver);
            await _tripRepo.SaveChangesAsync(); // thực tế cần UnitOfWork để save cả 2 cùng lúc
            await _driverRepo.SaveChangesAsync();
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
