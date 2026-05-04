﻿using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Domain.Repositories;
using Domain.Strategies;
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
            await _fareRuleRepo.InitializeAsync();
            await _fareRuleRepo.EnsureSeededAsync();
        }

        /// <summary>
        /// Calculates fare using fare rules configured by admin.
        /// </summary>
        public async Task<Fare> CalculateFareAsync(VehicleType vehicleType, double distanceKm)
        {
            FareRule rule = await _fareRuleRepo.GetByVehicleTypeAsync(vehicleType);
            if (rule == null)
            {
                throw new InvalidOperationException("Chưa cấu hình quy tắc giá cho loại xe này.");
            }

            IFareCalculationStrategy strategy;
            switch (vehicleType)
            {
                case VehicleType.Car:
                    strategy = new CarFareStrategy(rule);
                    break;
                case VehicleType.Motorbike:
                    strategy = new MotorbikeFareStrategy(rule);
                    break;
                default:
                    throw new InvalidOperationException("Loại xe không được hỗ trợ.");
            }

            return strategy.CalculateFare(distanceKm);
        }
    }
}
