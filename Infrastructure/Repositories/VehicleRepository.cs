// Infrastructure/Repositories/VehicleRepository.cs
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class VehicleRepository : JsonRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository() : base("vehicles.json") { }

        public async Task<List<Vehicle>> GetByTypeAsync(VehicleType type)
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.Where(v => v.Type == type).ToList();
            }
            finally
            {
                _fileLock.Release();
            }
        }
    }
}