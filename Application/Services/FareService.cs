using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Infrastructure.Interfaces;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FareService : IFareService
    {
        private readonly IFareRuleRepository _fareRuleRepo;

       public FareService(IFareRuleRepository fareRuleRepo)
        {
            _fareRuleRepo = fareRuleRepo;
        }
        public async Task SeedDefaultFareRulesAsync()
        {
            await _fareRuleRepo.InitializeAsync(); // load hiện có
            await _fareRuleRepo.EnsureSeededAsync();
        }

        public async Task<Fare> CalculateFare(VehicleType vehicleType, double distanceKm)
        {
            FareRule rule = await _fareRuleRepo.GetByVehicleTypeAsync(vehicleType);
            if (rule == null)
                throw new Exception($"Chưa có quy tắc giá cho loại xe {vehicleType}.");
            return rule.CalculateFare(distanceKm);
        }
    }
}
