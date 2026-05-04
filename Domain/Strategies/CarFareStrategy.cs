using Domain.Entities;
using Domain.ValueObjects;
using System;

namespace Domain.Strategies
{
    /// <summary>
    /// Concrete strategy for car fare calculation.
    /// </summary>
    public class CarFareStrategy : IFareCalculationStrategy
    {
        private readonly FareRule _fareRule;

        public CarFareStrategy(FareRule fareRule)
        {
            _fareRule = fareRule ?? throw new ArgumentNullException(nameof(fareRule));
        }

        public Fare CalculateFare(double distanceKm)
        {
            return _fareRule.CalculateFare(distanceKm);
        }
    }
}