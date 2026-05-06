using Domain.Entities.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IDriverRepository : IRepository<Driver>
    {
        Task<Driver> GetByPhoneAsync(string phone);
        Task<List<Driver>> GetAvailableDriversAsync();  
        Task<bool> ExistsByPhoneAsync(string phone);
    }
}