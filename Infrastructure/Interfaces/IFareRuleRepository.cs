using Domain.Enums;
using Domain.FareRules;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IFareRuleRepository : IRepository<FareRule>
    {
        Task<FareRule> GetByVehicleTypeAsync(VehicleType vehicleType);
        Task EnsureSeededAsync();
    }
}