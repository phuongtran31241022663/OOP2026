using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Users;
using Domain.Enums;
using Domain.Repositories;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IPassengerRepository _passengerRepository;

        public UserService(IDriverRepository driverRepository, IPassengerRepository passengerRepository)
        {
            _driverRepository = driverRepository;
            _passengerRepository = passengerRepository;
        }

        public async Task<User> LoginAsync(string phone, string password)
        {
            Driver driver = await _driverRepository.GetByPhoneAsync(phone);
            if (driver != null && driver.VerifyPassword(password))
                return driver;

            Passenger passenger = await _passengerRepository.GetByPhoneAsync(phone);
            if (passenger != null && passenger.VerifyPassword(password))
                return passenger;

            throw new Exception("Sai số điện thoại hoặc mật khẩu.");
        }

        public async Task RegisterDriverAsync(string name, string phone, string password,
            string licenseNumber, Guid vehicleId, Location initialPosition)
        {
            bool driverExists = await _driverRepository.ExistsByPhoneAsync(phone);
            bool passengerExists = await _passengerRepository.ExistsByPhoneAsync(phone);
            if (driverExists || passengerExists)
                throw new Exception("Số điện thoại đã được đăng ký.");

            Driver driver = new Driver(name, phone, password, licenseNumber, vehicleId, initialPosition);
            await _driverRepository.AddAsync(driver);
            await _driverRepository.SaveChangesAsync();
        }

        public async Task RegisterPassengerAsync(string name, string phone, string password)
        {
            bool driverExists = await _driverRepository.ExistsByPhoneAsync(phone);
            bool passengerExists = await _passengerRepository.ExistsByPhoneAsync(phone);
            if (driverExists || passengerExists)
                throw new Exception("Số điện thoại đã được đăng ký.");

            Passenger passenger = new Passenger(name, phone, password);
            await _passengerRepository.AddAsync(passenger);
            await _passengerRepository.SaveChangesAsync();
        }

        public async Task<Driver> GetDriverByIdAsync(Guid driverId)
        {
            Driver driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null) throw new Exception("Driver not found.");
            return driver;
        }

        public async Task<Passenger> GetPassengerByIdAsync(Guid passengerId)
        {
            Passenger passenger = await _passengerRepository.GetByIdAsync(passengerId);
            if (passenger == null) throw new Exception("Passenger not found.");
            return passenger;
        }

        public async Task UpdateDriverStatusAsync(Guid driverId, string newStatus)
        {
            Driver driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null) throw new Exception("Driver not found.");
            switch (newStatus)
            {
                case "Available":
                    driver.SetAvailable();
                    break;
                case "Offline":
                    driver.SetOffline();
                    break;
                case "OnTrip":
                    driver.SetOnTrip();
                    break;
            }
            await _driverRepository.UpdateAsync(driver);
            await _driverRepository.SaveChangesAsync();
        }

        public async Task UpdateDriverLocationAsync(Guid driverId, Location location)
        {
            Driver driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null) throw new Exception("Driver not found.");
            driver.UpdatePosition(location);
            await _driverRepository.UpdateAsync(driver);
            await _driverRepository.SaveChangesAsync();
        }

        public async Task<List<Driver>> GetAvailableDriversAsync()
        {
            return await _driverRepository.GetAvailableDriversAsync();
        }

        public async Task<bool> DriverExistsAsync(Guid driverId)
        {
            Driver driver = await _driverRepository.GetByIdAsync(driverId);
            return driver != null;
        }
    }
}