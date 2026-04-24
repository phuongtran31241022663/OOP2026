using Application.DTOs;
using Domain.Enums;
using Domain.Users;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<User> Login(string phone, string password);
        Task RegisterDriver(string name, string phone, string password,
            string licenseNumber, Guid vehicleId, Location initialPosition);
        Task RegisterPassenger(string name, string phone, string password);
        Task<DriverDto> GetDriverById(Guid driverId);
        Task<PassengerDto> GetPassengerById(Guid passengerId);
        Task UpdateDriverStatus(Guid driverId, DriverStatus newStatus);
        Task UpdateDriverLocation(Guid driverId, Location location);
        Task<IEnumerable<DriverDto>> GetAvailableDrivers();
        Task<bool> DriverExists(Guid driverId);
    }
}
