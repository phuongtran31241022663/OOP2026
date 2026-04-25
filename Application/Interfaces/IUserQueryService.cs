using Domain.Entities; 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserQueryService
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByPhoneAsync(string phone);
        Task<List<User>> GetAllAsync(); 
        Task<bool> ExistsByPhoneAsync(string phone);
    }
}