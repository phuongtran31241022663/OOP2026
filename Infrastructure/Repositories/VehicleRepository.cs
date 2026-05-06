using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;


// Infrastructure/Repositories/VehicleRepository.cs

namespace Infrastructure.Repositories
{
    public class VehicleRepository : JsonRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository() : base("vehicles.json") { }

        public async Task<List<Vehicle>> GetByTypeAsync(string type)
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.Where(v => string.Equals(v.TypeName, type, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            finally
            {
                _fileLock.Release();
            }
        }
    }
}


