using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities.Users;
using Domain.Repositories;
using Domain.ValueObjects;
﻿using System;

namespace Application.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<Driver> GetDriverAsync(Guid driverId)
        {
            Driver driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null) throw new InvalidOperationException("Không tìm thấy tài xế.");
            return driver;
        }


        public async Task UpdateLocationAsync(Guid driverId, Location newLocation)
        {
            Driver driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null) throw new InvalidOperationException("Không tìm thấy tài xế.");
            driver.UpdatePosition(newLocation);

            await _driverRepository.UpdateAsync(driver);
            await _driverRepository.SaveChangesAsync();
        }

        public async Task SetAvailableAsync(Guid driverId)
        {
            Driver driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null) throw new InvalidOperationException("Không tìm thấy tài xế.");
            driver.SetAvailable();

            await _driverRepository.UpdateAsync(driver);
            await _driverRepository.SaveChangesAsync();
        }

        public async Task SetOfflineAsync(Guid driverId)
        {
            Driver driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null) throw new InvalidOperationException("Không tìm thấy tài xế.");
            driver.SetOffline();

            await _driverRepository.UpdateAsync(driver);
            await _driverRepository.SaveChangesAsync();
        }

        public async Task<List<Driver>> GetAvailableDriversAsync()
        {
            return await _driverRepository.GetAvailableDriversAsync();
        }
    }
}

