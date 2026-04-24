using Domain.Vehicles;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class VehicleRepository : JsonRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository() : base("vehicles.json") { }

        public async Task<Vehicle> GetByPlateNumberAsync(string plateNumber)
        {
            await Task.CompletedTask;
            return _items.FirstOrDefault(v => v.PlateNumber == plateNumber);
        }

        public async Task<IEnumerable<Vehicle>> GetByTypeAsync(VehicleType type)
        {
            await Task.CompletedTask;
            return _items.Where(v => v.Type == type);
        }

        public async Task<bool> ExistsByPlateNumberAsync(string plateNumber)
        {
            await Task.CompletedTask;
            return _items.Any(v => v.PlateNumber == plateNumber);
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            Add(vehicle);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            Update(vehicle);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                Delete(entity);
            await Task.CompletedTask;
        }
    }
}