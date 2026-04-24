using Domain.SharedKernel;
using Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPassengerRepository : IRepository<Passenger>
    {
        Task<Passenger> GetByPhoneAsync(string phone);
        Task<bool> ExistsByPhoneAsync(string phone);
    }
}