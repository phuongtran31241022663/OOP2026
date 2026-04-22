using Application.Interfaces;
using Domain.Enums;
using Domain.FareRules;
using Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FareRuleService : IFareRuleService
    {
        private readonly IFareRuleRepository _fareRuleRepository;
        private readonly IRouteService _routeService;

        public FareRuleService(IFareRuleRepository fareRuleRepository, IRouteService routeService)
        {
            _fareRuleRepository = fareRuleRepository;
            _routeService = routeService;
        }

        public Task<Fare> CalculateFare(VehicleType vehicleType)
        {
            if (_routeService. == null)
                throw new ArgumentNullException(nameof(route));

            FareRule rule = await _fareRuleRepository.GetByVehicleType(vehicleType);
            if (rule == null)
                throw new InvalidOperationException($"Không tìm thấy fare rule cho {vehicleType}.");

            return rule.CalculateFare(route.Distance);
            var rule = GetFareRule(vehicleType);
            var fare = rule.CalculateFare(distanceKm);
            return fare.TotalAmount.Amount;
        }
    }
}
