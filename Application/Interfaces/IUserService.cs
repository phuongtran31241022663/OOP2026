using Application.DTOs;
using Domain.Enums;
using Domain.Users.Drivers;
using Domain.Users.Passengers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Login(string phone, string password);
        Task Register(string name, string phone, string password, bool isDriver, object vehicleInfo = null);
        Task<DriverDto> GetDriverById(Guid driverId);
        Task<PassengerDto> GetPassengerById(Guid passengerId);
        Task UpdateDriverStatus(Guid driverId, DriverStatus newStatus);
        Task UpdateDriverLocation(Guid driverId, Domain.ValueObjects.Location location);
        Task<IEnumerable<DriverDto>> GetAvailableDrivers();
        Task<bool> DriverExists(Guid driverId);
    }
}
