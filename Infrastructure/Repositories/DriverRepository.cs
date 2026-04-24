using Domain.Users.Drivers;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DriverRepository : JsonRepository<Driver>, IDriverRepository
    {
        public DriverRepository() : base("drivers.json") { }

        public async Task<Driver> GetByPhoneAsync(string phone)
        {
            await Task.CompletedTask;
            return _items.FirstOrDefault(d => d.Phone == phone);
        }

        public async Task<IEnumerable<Driver>> GetAvailableDriversAsync()
        {
            await Task.CompletedTask;
            return _items.Where(d => d.Status == DriverStatus.Available);
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            await Task.CompletedTask;
            return _items.Any(d => d.Phone == phone);
        }

        public async Task AddAsync(Driver driver)
        {
            Add(driver);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Driver driver)
        {
            Update(driver);
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