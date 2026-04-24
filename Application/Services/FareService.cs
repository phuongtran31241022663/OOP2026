using Application.Interfaces;
using Domain.Enums;
using Domain.FareRules;
using Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FareService : IFareService
    {
        private readonly IFareRuleRepository _fareRuleRepository;
        private readonly IRouteService _routeService;

        public FareService(IFareRuleRepository fareRuleRepository, IRouteService routeService)
        {
            _fareRuleRepository = fareRuleRepository;
            _routeService = routeService;
        }

        public async Task SeedDefaultFareRulesAsync()
        {
            await _fareRuleRepo.InitializeAsync(); // load hiện có
            await _fareRuleRepo.EnsureSeededAsync();
        }

        public async Task<Fare> CalculateFare(VehicleType vehicleType, double distanceKm)
        {
            var rule = await _fareRuleRepo.GetByVehicleTypeAsync(vehicleType);
            if (rule == null)
                throw new Exception($"Chưa có quy tắc giá cho loại xe {vehicleType}.");

            return rule.CalculateFare(distanceKm);
        }
    }
}
