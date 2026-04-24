using Domain.Users;
using Domain.Users.Drivers;
using Domain.Users.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : JsonRepository<User>, IUserRepository
    {
        public UserRepository() : base("users.json") { }

        public async Task<Driver> GetDriverByIdAsync(Guid id)
        {
            await Task.CompletedTask;
            return _items.OfType<Driver>().FirstOrDefault(d => d.Id == id);
        }

        public async Task<User> GetByPhoneAsync(string phone)
        {
            await Task.CompletedTask;
            return _items.FirstOrDefault(u => u.Phone == phone);
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            await Task.CompletedTask;
            return _items.Any(u => u.Phone == phone);
        }

        public async Task<IEnumerable<User>> GetDriversAsync()
        {
            await Task.CompletedTask;
            return _items.OfType<Driver>();
        }

        public async Task<IEnumerable<User>> GetPassengersAsync()
        {
            await Task.CompletedTask;
            return _items.Where(u => u is Passenger);
        }

        public async Task<IEnumerable<User>> GetAvailableDriversAsync()
        {
            await Task.CompletedTask;
            return _items.OfType<Driver>().Where(d => d.Status == Domain.Enums.DriverStatus.Available);
        }

        public async Task AddAsync(User user)
        {
            Add(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            Update(user);
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