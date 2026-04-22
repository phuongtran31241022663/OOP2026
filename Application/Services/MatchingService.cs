using Domain.Enums;
using Domain.Trips;
using Domain.Users;
using Domain.Users.Drivers;
using System;
using System.Threading.Tasks;

namespace Domain.Services
{
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
            // 1. Retrieve the Trip and the Driver
            Trip trip = await _tripRepository.GetByIdAsync(tripId);
            Driver driver = await _userRepository.GetDriverByIdAsync(driverId);

            // 2. Domain Validation
            if (trip == null || driver == null) return false;

            // Check if the driver is currently available
            if (driver.Status != DriverStatus.Available)
            {
                return false;
            }

            // 3. Execute the Matching Logic
            // This calls a method on the Trip aggregate to handle internal state changes
            trip.MatchDriver(driver.Id);

            if (success)
            {
                // 4. Persist changes via repositories
                await _tripRepository.UpdateAsync(trip);
                return true;
            }

            return false;
        }
    }
}