using Domain.SharedKernel;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    /// <summary>
    /// Repository interface for Driver aggregate.
    /// Giao diện repository cho aggregate Driver.
    /// </summary>
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<Driver> GetByPhoneAsync(string phone);
        Task<IEnumerable<Driver>> GetAvailableDriversAsync();
        Task<bool> ExistsByPhoneAsync(string phone);
    }
}
