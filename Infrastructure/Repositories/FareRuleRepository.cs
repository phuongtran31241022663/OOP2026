using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Interfaces;

namespace Infrastructure.Repositories
{
    public class FareRuleRepository : JsonRepository<FareRule>, IFareRuleRepository
    {
        public FareRuleRepository() : base("farerule.json") { }

        public async Task<FareRule> GetByVehicleTypeAsync(VehicleType vehicleType)
        {
            await Task.CompletedTask;
            return _items.FirstOrDefault(r => r.VehicleType == vehicleType);
        }

        public async Task EnsureSeededAsync()
        {
            await InitializeAsync();

            if (!_items.Any(r => r.VehicleType == VehicleType.Motorbike))
            {
                Add(new FareRule(
                    VehicleType.Motorbike,
                    new Money(15000, "VND"),
                    new Money(3000, "VND"),
                    0.2m
                ));
            }

            if (!_items.Any(r => r.VehicleType == VehicleType.Car))
            {
                Add(new FareRule(
                    VehicleType.Car,
                    new Money(25000, "VND"),
                    new Money(5000, "VND"),
                    0.2m
                ));
            }

            await SaveChangesAsync();
        }

        public async Task AddAsync(FareRule rule)
        {
            Add(rule);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(FareRule rule)
        {
            Update(rule);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
                Delete(entity);
            await Task.CompletedTask;
        }
    }
}