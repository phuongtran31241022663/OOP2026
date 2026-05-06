using Domain.Entities.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
﻿// Infrastructure/Repositories/DriverRepository.cs

namespace Infrastructure.Repositories
{
    public class DriverRepository : JsonRepository<Driver>, IDriverRepository
    {
        public DriverRepository() : base("drivers.json") { }

        public async Task<Driver> GetByPhoneAsync(string phone)
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.FirstOrDefault(d => d.Phone == phone);
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task<List<Driver>> GetAvailableDriversAsync()
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.Where(d => d.IsAvailable()).ToList();
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.Any(d => d.Phone == phone);
            }
            finally
            {
                _fileLock.Release();
            }
        }
    }
}

