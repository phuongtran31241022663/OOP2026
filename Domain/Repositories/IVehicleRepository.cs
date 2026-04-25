using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IVehicleRepository
    {
        Task InitializeAsync();
        Task SaveChangesAsync();
        Task<Vehicle> GetByIdAsync(Guid id);
        Task<List<Vehicle>> GetAllAsync();
        Task<List<Vehicle>> GetByTypeAsync(VehicleType type);
        Task AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Guid id);
    }
}