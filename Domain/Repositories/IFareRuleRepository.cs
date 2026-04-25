using Domain.Enums;
using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IFareRuleRepository : IRepository<FareRule>
    {
        Task<FareRule> GetByVehicleTypeAsync(VehicleType vehicleType);
        Task EnsureSeededAsync();
    }
}