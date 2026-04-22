using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Users.Drivers
{
    /// <summary>
    /// Repository interface for Driver aggregate.
    /// Giao diện repository cho aggregate Driver.
    /// </summary>
    public interface IDriverRepository // Phải kế thừa IRepository<T>
    {
        Task InitializeAsync();
        Task SaveChangesAsync();
        Task<Driver> GetByIdAsync(Guid id);
        Task<IEnumerable<Driver>> GetAllAsync();
        Task<Driver> GetByPhoneAsync(string phone);
        Task<IEnumerable<Driver>> GetAvailableDriversAsync();
        Task<bool> ExistsByPhoneAsync(string phone);

        Task AddAsync(Driver driver);
        Task UpdateAsync(Driver driver);
        Task DeleteAsync(Guid id);
    }
}
