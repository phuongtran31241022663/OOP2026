using Domain.Users.Passengers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PassengerRepository : JsonRepository<Passenger>, IPassengerRepository
    {
        public PassengerRepository() : base("passengers.json") { }

        public async Task<Passenger> GetByPhoneAsync(string phone)
        {
            await Task.CompletedTask;
            return _items.FirstOrDefault(p => p.Phone == phone);
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            await Task.CompletedTask;
            return _items.Any(p => p.Phone == phone);
        }

        public async Task AddAsync(Passenger passenger)
        {
            Add(passenger);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Passenger passenger)
        {
            Update(passenger);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                Delete(entity);
            await Task.CompletedTask;
        }
    }
}