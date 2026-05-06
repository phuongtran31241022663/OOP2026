using Domain.Entities;
using Domain.ValueObjects;
using Domain.Repositories;
using Domain.Strategies;
using System;
using System.Threading.Tasks;


﻿using Application.Interfaces;

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
        public async Task<Fare> CalculateFareAsync(string vehicleType, double distanceKm)
        {
            if (string.IsNullOrEmpty(vehicleType))
                throw new ArgumentException("Vehicle type is required.", nameof(vehicleType));

            FareRule rule = await _fareRuleRepo.GetByVehicleTypeAsync(vehicleType);
            if (rule == null)
            {
                throw new InvalidOperationException($"Chưa cấu hình quy tắc giá cho loại xe '{vehicleType}'.");
            }

            IFareCalculationStrategy strategy;
            if (vehicleType.Equals("Car", StringComparison.OrdinalIgnoreCase))
            {
                strategy = new CarFareStrategy(rule);
            }
            else if (vehicleType.Equals("Motorbike", StringComparison.OrdinalIgnoreCase))
            {
                strategy = new MotorbikeFareStrategy(rule);
            }
            else
            {
                throw new InvalidOperationException($"Loại xe '{vehicleType}' không được hỗ trợ.");
            }

            return strategy.CalculateFare(distanceKm);
        }

    }
}


