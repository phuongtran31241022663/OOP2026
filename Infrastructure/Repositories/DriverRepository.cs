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

        public async Task<Driver> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_entities.FirstOrDefault(d => d.Id == id));
        }

        public async Task<IEnumerable<Driver>> GetAllAsync()
        {
            return await Task.FromResult(_entities);
        }

        public async Task<Driver> GetByPhoneAsync(string phone)
        {
            return await Task.FromResult(_entities.FirstOrDefault(d => d.Phone == phone));
        }

        public async Task<IEnumerable<Driver>> GetAvailableDriversAsync()
        {
            return await Task.FromResult(_entities.Where(d => d.Status == DriverStatus.Available));
        }

        public async Task AddAsync(Driver driver)
        {
            _entities.Add(driver);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Driver driver)
        {
            var index = _entities.FindIndex(d => d.Id == driver.Id);
            if (index != -1)
                _entities[index] = driver;

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _entities.FirstOrDefault(d => d.Id == id);
            if (entity != null)
                _entities.Remove(entity);

            await Task.CompletedTask;
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            return await Task.FromResult(_entities.Any(d => d.Phone == phone));
        }
    }
}