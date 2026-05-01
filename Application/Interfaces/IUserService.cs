﻿using Domain.Entities; 
using Domain.Entities.Users;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<User> LoginAsync(string phone, string password);
        Task RegisterDriverAsync(string name, string phone, string password,
            string licenseNumber, Guid vehicleId, Location initialPosition);
        Task RegisterPassengerAsync(string name, string phone, string password);
        Task<Driver> GetDriverByIdAsync(Guid driverId); // trả về Driver entity
        Task<Passenger> GetPassengerByIdAsync(Guid passengerId);
        Task UpdateDriverStatusAsync(Guid driverId, string newStatus);
        Task UpdateDriverLocationAsync(Guid driverId, Location location);
        Task<List<Driver>> GetAvailableDriversAsync(); 
        Task<bool> DriverExistsAsync(Guid driverId);
        Task UpdateUserAsync(User user);
    }
}
