using Application.DTOs;
using Application.Interfaces;
using Domain.Users;
using Domain.Users.Drivers;
using Domain.Users.Passengers;
using Domain.ValueObjects;
using Domain.Users.Drivers.Vehicles;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDriverRepository _driverRepository;
        private readonly IPassengerRepository _passengerRepository;

        public UserService(
            IUserRepository userRepository,
            IDriverRepository driverRepository,
            IPassengerRepository passengerRepository)
        {
            _userRepository = userRepository;
            _driverRepository = driverRepository;
            _passengerRepository = passengerRepository;
        }

        public async Task<UserDto> Login(string phone, string password)
        {
            if (string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Phone and password required.");

            // Try driver
            var driver = _driverRepository.GetByPhone(phone);
            if (driver != null && driver.VerifyPassword(password))
            {
                return new UserDto
                {
                    Id = driver.Id,
                    Name = driver.Name,
                    Phone = driver.Phone,
                    UserType = "Driver",
                    IsActive = driver.IsActive
                };
            }

            // Try passenger
            var passenger = _passengerRepository.GetByPhone(phone);
            if (passenger != null && passenger.VerifyPassword(password))
            {
                return new UserDto
                {
                    Id = passenger.Id,
                    Name = passenger.Name,
                    Phone = passenger.Phone,
                    UserType = "Passenger",
                    IsActive = passenger.IsActive
                };
            }

            throw new Exception("Sai số điện thoại hoặc mật khẩu.");
        }

        public async Task Register(string name, string phone, string password, bool isDriver, object vehicleInfo = null)
        {
            // Check if phone exists
            if (_driverRepository.GetByPhone(phone) != null || _passengerRepository.GetByPhone(phone) != null)
                throw new InvalidOperationException("Số điện thoại đã được đăng ký.");

            if (isDriver)
            {
                if (vehicleInfo == null)
                    throw new ArgumentException("Vehicle info required for driver registration.");

                if (vehicleInfo is Vehicle vehicle)
                {
                    var defaultLocation = new Location("Default", new Address("Unknown", "", "", ""), 0, 0);
                    var driver = new Driver(name, phone, password, vehicle, defaultLocation);
                    _driverRepository.Add(driver);
                    await _driverRepository.SaveChangesAsync();
                }
                else
                {
                    throw new ArgumentException("Invalid vehicle information.");
                }
            }
            else
            {
                var passenger = new Passenger(name, phone, password);
                _passengerRepository.Add(passenger);
                await _passengerRepository.SaveChangesAsync();
            }
        }

        public async Task<DriverDto> GetDriverById(Guid driverId)
        {
            var driver = _driverRepository.GetById(driverId);
            if (driver == null) throw new Exception("Tài xế không tồn tại.");
            return DriverMapper.ToDto(driver);
        }

        public async Task<PassengerDto> GetPassengerById(Guid passengerId)
        {
            var passenger = _passengerRepository.GetById(passengerId);
            if (passenger == null) throw new Exception("Hành khách không tồn tại.");
            return PassengerMapper.ToDto(passenger);
        }

        public async Task UpdateDriverStatus(Guid driverId, DriverStatus newStatus)
        {
            var driver = _driverRepository.GetById(driverId);
            if (driver == null) throw new Exception("Tài xế không tồn tại.");

            switch (newStatus)
            {
                case DriverStatus.Available:
                    driver.SetAvailable();
                    break;
                case DriverStatus.Offline:
                    driver.SetOffline();
                    break;
                case DriverStatus.OnTrip:
                    throw new InvalidOperationException("Use trip assignment to set driver OnTrip.");
                default:
                    throw new NotSupportedException($"Trạng thái {newStatus} không được hỗ trợ.");
            }

            _driverRepository.Update(driver);
            await _driverRepository.SaveChangesAsync();
        }

        public async Task UpdateDriverLocation(Guid driverId, Location location)
        {
            var driver = _driverRepository.GetById(driverId);
            if (driver == null) throw new Exception("Tài xế không tồn tại.");
            driver.UpdatePosition(location);
            _driverRepository.Update(driver);
            await _driverRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<DriverDto>> GetAvailableDrivers()
        {
            var drivers = _driverRepository.GetAll().Where(d => d.Status == DriverStatus.Available);
            return drivers.Select(d => DriverMapper.ToDto(d)).ToList();
        }

        public async Task<bool> DriverExists(Guid driverId)
        {
            return _driverRepository.GetById(driverId) != null;
        }
    }
}
