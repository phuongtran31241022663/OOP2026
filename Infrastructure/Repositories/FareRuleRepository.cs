using Domain.Entities;
using Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;



// Infrastructure/Repositories/FareRuleRepository.cs

namespace Infrastructure.Repositories
{
    public class FareRuleRepository : JsonRepository<FareRule>, IFareRuleRepository
    {
        public FareRuleRepository() : base("farerule.json") { }

        public async Task<FareRule> GetByVehicleTypeAsync(string vehicleType)
        {
            if (string.IsNullOrEmpty(vehicleType))
                throw new ArgumentException("Vehicle type is required.", nameof(vehicleType));

            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.FirstOrDefault(r => string.Equals(r.VehicleType, vehicleType, StringComparison.OrdinalIgnoreCase));
            }
            finally
            {
                _fileLock.Release();
            }
        }


        public async Task EnsureSeededAsync()
        {
            await InitializeAsync();
            await _fileLock.WaitAsync();
            try
            {
                if (!_items.Any(r => string.Equals(r.VehicleType, "Motorbike", StringComparison.OrdinalIgnoreCase)))
                {
                    _items.Add(new FareRule(
                        "Motorbike",
                        new Money(15000, "VND"),
                        new Money(3000, "VND"),
                        0.2m
                    ));
                }

                if (!_items.Any(r => string.Equals(r.VehicleType, "Car", StringComparison.OrdinalIgnoreCase)))
                {
                    _items.Add(new FareRule(
                        "Car",
                        new Money(25000, "VND"),
                        new Money(5000, "VND"),
                        0.2m
                    ));
                }

            }
            finally
            {
                _fileLock.Release();
            }

            await SaveChangesAsync();
        }
    }
}

