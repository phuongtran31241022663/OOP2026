using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Entities;

namespace Domain.Repositories
{
    /// <summary>
    /// Repository interface for Vehicle aggregate.
    /// Giao diện repository cho aggregate Vehicle.
    /// </summary>
    public interface IVehicleRepository
    {
        Task InitializeAsync();
        Task SaveChangesAsync();
        Task<Vehicle> GetByIdAsync(Guid id);
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task<IEnumerable<Vehicle>> GetByTypeAsync(VehicleType type);
        Task AddAsync(Vehicle vehicle);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Guid id);
    }
}
