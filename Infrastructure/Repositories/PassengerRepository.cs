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

        public async Task<Passenger> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_entities.FirstOrDefault(p => p.Id == id));
        }

        public async Task<IEnumerable<Passenger>> GetAllAsync()
        {
            return await Task.FromResult(_entities);
        }

        public async Task<Passenger> GetByPhoneAsync(string phone)
        {
            return await Task.FromResult(_entities.FirstOrDefault(p => p.Phone == phone));
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            return await Task.FromResult(_entities.Any(p => p.Phone == phone));
        }

        public async Task AddAsync(Passenger passenger)
        {
            _entities.Add(passenger);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Passenger passenger)
        {
            var index = _entities.FindIndex(p => p.Id == passenger.Id);
            if (index != -1)
                _entities[index] = passenger;

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _entities.FirstOrDefault(p => p.Id == id);
            if (entity != null)
                _entities.Remove(entity);

            await Task.CompletedTask;
        }
    }
}