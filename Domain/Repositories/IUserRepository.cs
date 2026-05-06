using Domain.Entities;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task InitializeAsync();
        Task SaveChangesAsync();

        Task<User> GetByIdAsync(Guid id);
        Task<Driver> GetDriverByIdAsync(Guid id);
        Task<List<User>> GetAllAsync();
        Task<User> GetByPhoneAsync(string phone);
        Task<bool> ExistsByPhoneAsync(string phone);

        Task<List<User>> GetDriversAsync();
        Task<List<User>> GetPassengersAsync();
        Task<List<Driver>> GetAvailableDriversAsync();

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}