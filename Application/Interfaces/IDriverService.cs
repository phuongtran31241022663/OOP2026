using Domain.Entities.Users;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDriverService
    {
        Task<Driver> GetDriverAsync(Guid driverId);
        Task UpdateLocationAsync(Guid driverId, Location newLocation);
        Task SetAvailableAsync(Guid driverId);
        Task SetOfflineAsync(Guid driverId);
        Task<List<Driver>> GetAvailableDriversAsync();
    }
}