using Domain.Enums;
using Domain.Entities;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Infrastructure.Interfaces
{
    public interface IFareRuleRepository : IRepository<FareRule>
    {
        Task<FareRule> GetByVehicleTypeAsync(VehicleType vehicleType);
        Task EnsureSeededAsync();
    }
}