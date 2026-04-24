using Domain.Enums;
using Domain.Users.Drivers;
using Domain.ValueObjects;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services
{
    public class DriverService
    {
        private readonly DriverRepository _driverRepo;

        public DriverService(DriverRepository driverRepo)
        {
            _driverRepo = driverRepo;
        }

        public async Task UpdateLocationAsync(Guid driverId, Location newLocation)
        {
            var driver = await _driverRepo.GetByIdAsync(driverId) ?? throw new Exception("Driver not found.");
            driver.UpdatePosition(newLocation);
            _driverRepo.Update(driver);
            await _driverRepo.SaveChangesAsync();
        }

        public async Task SetAvailableAsync(Guid driverId)
        {
            var driver = await _driverRepo.GetByIdAsync(driverId) ?? throw new Exception("Driver not found.");
            driver.SetAvailable();
            _driverRepo.Update(driver);
            await _driverRepo.SaveChangesAsync();
        }

        public async Task SetOfflineAsync(Guid driverId)
        {
            var driver = await _driverRepo.GetByIdAsync(driverId) ?? throw new Exception("Driver not found.");
            driver.SetOffline();
            _driverRepo.Update(driver);
            await _driverRepo.SaveChangesAsync();
        }

        public async Task<List<Driver>> GetAvailableDriversAsync()
        {
            return (await _driverRepo.GetAvailableDriversAsync()).ToList();
        }

        public async Task<Driver> GetDriverAsync(Guid driverId) => await _driverRepo.GetByIdAsync(driverId);
    }
}