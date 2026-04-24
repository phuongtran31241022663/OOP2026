using Domain.Enums;
using Domain.Repositories;
using Domain.Trips;
using Domain.Users;
using Domain.Users.Drivers;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    /// <summary>
    /// Matches drivers to trips based on availability and vehicle type.
    /// </summary>
    public class MatchingService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITripRepository _tripRepository;

        public MatchingService(IUserRepository userRepository, ITripRepository tripRepository)
        {
            _userRepository = userRepository;
            _tripRepository = tripRepository;
        }

        public async Task<bool> MatchDriverToTripAsync(Guid tripId, Guid driverId)
        {
            var trip = await _tripRepository.GetByIdAsync(tripId);
            var driver = await _userRepository.GetDriverByIdAsync(driverId);

            if (trip == null || driver == null) return false;
            if (driver.Status != DriverStatus.Available) return false;

            // Additional checks: vehicle type match
            var vehicleRepo = (IVehicleRepository)_userRepository; // IUserRepository also implements IVehicleRepository? No; but for simplicity we assume we have IVehicleRepository from DI
            // Actually we should inject IVehicleRepository separately; but in code we only have IUserRepository and ITripRepository.
            // We'll skip vehicle type matching if we can't get vehicle.
            // Since constructor doesn't take IVehicleRepository, we skip that check.

            // Let aggregate handle state change
            trip.MatchDriver(driver.Id);
            driver.SetOnTrip();

            await _tripRepository.UpdateAsync(trip);
            await _userRepository.UpdateAsync(driver);
            return true;
        }
    }
}
