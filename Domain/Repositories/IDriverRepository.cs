using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<Driver> GetByPhoneAsync(string phone);
        Task<List<Driver>> GetAvailableDriversAsync();  // List<Driver>
        Task<bool> ExistsByPhoneAsync(string phone);
    }
}