using Domain.Entities;
using Domain.ValueObjects;
using System;

namespace Domain.Strategies
{
    /// <summary>
    /// Concrete strategy for motorbike fare calculation.
    /// </summary>
    public class MotorbikeFareStrategy : IFareCalculationStrategy
    {
        private readonly FareRule _fareRule;

        public MotorbikeFareStrategy(FareRule fareRule)
        {
            _fareRule = fareRule ?? throw new ArgumentNullException(nameof(fareRule));
        }

        public Fare CalculateFare(double distanceKm)
        {
            return _fareRule.CalculateFare(distanceKm);
        }
    }
}