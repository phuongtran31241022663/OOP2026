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

        public async Task<Vehicle> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_entities.FirstOrDefault(v => v.Id == id));
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await Task.FromResult(_entities);
        }

        public async Task<Vehicle> GetByPlateNumberAsync(string plateNumber)
        {
            return await Task.FromResult(_entities.FirstOrDefault(v => v.PlateNumber == plateNumber));
        }

        public async Task<IEnumerable<Vehicle>> GetByTypeAsync(VehicleType type)
        {
            return await Task.FromResult(_entities.Where(v => v.Type == type));
        }

        public async Task<bool> ExistsByPlateNumberAsync(string plateNumber)
        {
            return await Task.FromResult(_entities.Any(v => v.PlateNumber == plateNumber));
        }

        public async Task AddAsync(Vehicle vehicle)
        {
            _entities.Add(vehicle);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Vehicle vehicle)
        {
            var index = _entities.FindIndex(v => v.Id == vehicle.Id);
            if (index != -1)
                _entities[index] = vehicle;

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _entities.FirstOrDefault(v => v.Id == id);
            if (entity != null)
                _entities.Remove(entity);

            await Task.CompletedTask;
        }
    }
}