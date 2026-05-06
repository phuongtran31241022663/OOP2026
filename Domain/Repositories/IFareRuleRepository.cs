using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IFareRuleRepository : IRepository<FareRule>
    {
        Task<FareRule> GetByVehicleTypeAsync(string vehicleType);
        Task EnsureSeededAsync();
    }
}