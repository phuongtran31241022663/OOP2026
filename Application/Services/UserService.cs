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
            if (driver != null)
            {
                // Kiểm tra null _password trong driver
                if (string.IsNullOrEmpty(driver.Password))
                    throw new UnauthorizedAccessException("Lỗi dữ liệu: tài xế không có mật khẩu. Vui lòng liên hệ admin.");

                if (driver.VerifyPassword(password))
                    return driver;
                else
                    throw new UnauthorizedAccessException("Sai mật khẩu. (Tài xế)");
            }

            Passenger passenger = await _passengerRepository.GetByPhoneAsync(phone);
            if (passenger != null)
            {
                if (string.IsNullOrEmpty(passenger.Password))
                    throw new UnauthorizedAccessException("Lỗi dữ liệu: hành khách không có mật khẩu. Vui lòng liên hệ admin.");

                if (passenger.VerifyPassword(password))
                    return passenger;
                else
                    throw new UnauthorizedAccessException("Sai mật khẩu. (Hành khách)");
            }

            throw new InvalidOperationException("Số điện thoại chưa được đăng ký.");
        }

        public async Task RegisterDriverAsync(string name, string phone, string password,
            string licenseNumber, Guid vehicleId, Location initialPosition)
        {
            bool driverExists = await _driverRepository.ExistsByPhoneAsync(phone);
            bool passengerExists = await _passengerRepository.ExistsByPhoneAsync(phone);
            if (driverExists || passengerExists)
                throw new InvalidOperationException("Số điện thoại đã được đăng ký.");

            Driver driver = new Driver(name, phone, password, licenseNumber, vehicleId, initialPosition);
            await _driverRepository.AddAsync(driver);
            await _driverRepository.SaveChangesAsync();
        }

        public async Task RegisterPassengerAsync(string name, string phone, string password)
        {
            bool driverExists = await _driverRepository.ExistsByPhoneAsync(phone);
            bool passengerExists = await _passengerRepository.ExistsByPhoneAsync(phone);
            if (driverExists || passengerExists)
                throw new InvalidOperationException("Số điện thoại đã được đăng ký.");

            Passenger passenger = new Passenger(name, phone, password);
            await _passengerRepository.AddAsync(passenger);
            await _passengerRepository.SaveChangesAsync();
        }

        public async Task<Driver> GetDriverByIdAsync(Guid driverId)
        {
            Driver driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null) throw new InvalidOperationException("Không tìm thấy tài xế.");
            return driver;
        }

        public async Task<Passenger> GetPassengerByIdAsync(Guid passengerId)
        {
            Passenger passenger = await _passengerRepository.GetByIdAsync(passengerId);
            if (passenger == null) throw new InvalidOperationException("Không tìm thấy hành khách.");
            return passenger;
        }

        public async Task UpdateDriverStatusAsync(Guid driverId, string newStatus)
        {
            Driver driver = await _driverRepository.GetByIdAsync(driverId);
            if (driver == null) throw new InvalidOperationException("Không tìm thấy tài xế.");
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
            if (driver == null) throw new InvalidOperationException("Không tìm thấy tài xế.");
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

        public async Task UpdateUserAsync(User user)
        {
            if (user is Driver driver)
            {
                await _driverRepository.UpdateAsync(driver);
                await _driverRepository.SaveChangesAsync();
            }
            else if (user is Passenger passenger)
            {
                await _passengerRepository.UpdateAsync(passenger);
                await _passengerRepository.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException("Unknown user type");
            }
        }
    }
}
