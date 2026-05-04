// Infrastructure/Repositories/FareRuleRepository.cs
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Infrastructure.Repositories
{
    public class FareRuleRepository : JsonRepository<FareRule>, IFareRuleRepository
    {
        public FareRuleRepository() : base("farerule.json") { }

        public async Task<FareRule> GetByVehicleTypeAsync(VehicleType vehicleType)
        {
            await EnsureLoadedAsync();
            await _fileLock.WaitAsync();
            try
            {
                return _items.FirstOrDefault(r => r.VehicleType == vehicleType);
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
                if (!_items.Any(r => r.VehicleType == VehicleType.Motorbike))
                {
                    _items.Add(new FareRule(
                        VehicleType.Motorbike,
                        new Money(15000, "VND"),
                        new Money(3000, "VND"),
                        0.2m
                    ));
                }

                if (!_items.Any(r => r.VehicleType == VehicleType.Car))
                {
                    _items.Add(new FareRule(
                        VehicleType.Car,
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