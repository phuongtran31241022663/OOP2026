// Infrastructure/Repositories/PassengerRepository.cs
using Domain.Entities.Users;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class PassengerRepository : JsonRepository<Passenger>, IPassengerRepository
    {
        public PassengerRepository() : base("passengers.json") { }

        public async Task<Passenger> GetByPhoneAsync(string phone)
        {
            await EnsureLoadedAsync();
            return _items.FirstOrDefault(p => p.Phone == phone);
        }

        public async Task<bool> ExistsByPhoneAsync(string phone)
        {
            await EnsureLoadedAsync();
            return _items.Any(p => p.Phone == phone);
        }
    }
}
