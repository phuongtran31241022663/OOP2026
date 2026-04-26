// Infrastructure/Repositories/UserRepository.cs
using Domain.Entities;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;
using Domain.Enums;

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

        public async Task<List<User>> GetDriversAsync()
        {
            await Task.CompletedTask;
            return _items.OfType<Driver>().Cast<User>().ToList();
        }

        public async Task<List<User>> GetPassengersAsync()
        {
            await Task.CompletedTask;
            return _items.Where(u => u is Passenger).ToList();
        }

        public async Task<List<Driver>> GetAvailableDriversAsync()
        {
            await Task.CompletedTask;
            return _items.OfType<Driver>().Where(d => d.Status == DriverStatus.Available).ToList();
        }
    }
}