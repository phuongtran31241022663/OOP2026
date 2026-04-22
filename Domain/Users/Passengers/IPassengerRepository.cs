using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Users.Passengers
{
    public interface IPassengerRepository
    {
        Task InitializeAsync();
        Task SaveChangesAsync();

        Task<Passenger> GetByIdAsync(Guid id);
        Task<IEnumerable<Passenger>> GetAllAsync();
        Task<Passenger> GetByPhoneAsync(string phone);

        Task<bool> ExistsByPhoneAsync(string phone);

        Task AddAsync(Passenger passenger);
        Task UpdateAsync(Passenger passenger);
        Task DeleteAsync(Guid id);
    }
}