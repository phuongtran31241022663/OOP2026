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
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByPhoneAsync(string phone);
        Task<bool> ExistsByPhoneAsync(string phone);

        Task<IEnumerable<User>> GetDriversAsync();
        Task<IEnumerable<User>> GetPassengersAsync();
        Task<IEnumerable<User>> GetAvailableDriversAsync();

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
    }
}