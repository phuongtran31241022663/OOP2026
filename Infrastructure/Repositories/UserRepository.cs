using Domain.Users;
using Domain.Users.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : JsonRepository<User>, IUserRepository
    {
        public UserRepository() : base("users.json") { }

        public async Task<User> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_entities.FirstOrDefault(u => u.Id == id));
        }

        public async Task<Driver> GetDriverByIdAsync(Guid id)
        {
            return await Task.FromResult(
                _entities.OfType<Driver>().FirstOrDefault(d => d.Id == id)
            );
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.FromResult(_entities);
        }

        public async Task<User> GetByPhoneAsync(string phone)
        {
            return await Task.FromResult(
                _entities.FirstOrDefault(u => u.Phone == phone)
            );
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            return await Task.FromResult(
                _entities.Any(u => u.Phone == phone)
            );
        }

        public async Task<IEnumerable<User>> GetDriversAsync()
        {
            return await Task.FromResult(
                _entities.OfType<Driver>()
            );
        }

        public async Task<IEnumerable<User>> GetPassengersAsync()
        {
            return await Task.FromResult(
                _entities.Where(u => u.GetType().Name == "Passenger")
            );
        }

        public async Task<IEnumerable<User>> GetAvailableDriversAsync()
        {
            return await Task.FromResult(
                _entities
                    .OfType<Driver>()
                    .Where(d => d.Status == Domain.Enums.DriverStatus.Available)
            );
        }

        public async Task AddAsync(User user)
        {
            _entities.Add(user);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(User user)
        {
            var index = _entities.FindIndex(u => u.Id == user.Id);
            if (index != -1)
                _entities[index] = user;

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _entities.FirstOrDefault(u => u.Id == id);
            if (entity != null)
                _entities.Remove(entity);

            await Task.CompletedTask;
        }
    }
}