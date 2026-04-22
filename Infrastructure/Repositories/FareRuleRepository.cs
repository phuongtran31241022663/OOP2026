using Domain.FareRules;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FareRuleRepository : JsonRepository<FareRule>, IFareRuleRepository
    {
        public FareRuleRepository() : base("farerule.json") { }

        public async Task<FareRule> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(_entities.FirstOrDefault(r => r.Id == id));
        }

        public async Task<IEnumerable<FareRule>> GetAllAsync()
        {
            return await Task.FromResult(_entities);
        }

        public async Task<FareRule> GetByVehicleTypeAsync(VehicleType vehicleType)
        {
            return await Task.FromResult(
                _entities.FirstOrDefault(r => r.VehicleType == vehicleType)
            );
        }

        public async Task AddAsync(FareRule rule)
        {
            _entities.Add(rule);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(FareRule rule)
        {
            var index = _entities.FindIndex(r => r.Id == rule.Id);
            if (index != -1)
                _entities[index] = rule;

            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = _entities.FirstOrDefault(r => r.Id == id);
            if (entity != null)
                _entities.Remove(entity);

            await Task.CompletedTask;
        }

        public async Task EnsureSeededAsync()
        {
            await InitializeAsync();

            if (!_entities.Any(r => r.VehicleType == VehicleType.Motorbike))
            {
                _entities.Add(new FareRule(
                    VehicleType.Motorbike,
                    new Money(15000),
                    new Money(3000),
                    0.2m
                ));
            }

            if (!_entities.Any(r => r.VehicleType == VehicleType.Car))
            {
                _entities.Add(new FareRule(
                    VehicleType.Car,
                    new Money(25000),
                    new Money(5000),
                    0.2m
                ));
            }

            await SaveChangesAsync();
        }
    }
}