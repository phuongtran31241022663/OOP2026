﻿// Infrastructure/Repositories/DriverRepository.cs
using Domain.Entities.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class DriverRepository : JsonRepository<Driver>, IDriverRepository
    {
        public DriverRepository() : base("drivers.json") { }

        public async Task<Driver> GetByPhoneAsync(string phone)
        {
            await EnsureLoadedAsync();
            return _items.FirstOrDefault(d => d.Phone == phone);
        }

        public async Task<List<Driver>> GetAvailableDriversAsync()
        {
            await EnsureLoadedAsync();
            return _items.Where(d => d.IsAvailable()).ToList();
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            await EnsureLoadedAsync();
            return _items.Any(d => d.Phone == phone);
        }
    }
}
