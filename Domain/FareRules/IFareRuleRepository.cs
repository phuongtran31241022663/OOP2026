using Domain.FareRules;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.FareRules
{
    public interface IFareRuleRepository
    {
        Task InitializeAsync();
        Task SaveChangesAsync();

        Task<FareRule> GetByIdAsync(Guid id);
        Task<IEnumerable<FareRule>> GetAllAsync();
        Task<FareRule> GetByVehicleTypeAsync(VehicleType vehicleType);

        Task AddAsync(FareRule rule);
        Task UpdateAsync(FareRule rule);
        Task DeleteAsync(Guid id);

        Task EnsureSeededAsync();
    }
}