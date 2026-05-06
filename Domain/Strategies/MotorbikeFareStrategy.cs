using Domain.Entities;
using Domain.ValueObjects;
using System;

namespace Domain.Strategies
{
/// <summary>
/// Concrete strategy for motorbike fare calculation.
/// </summary>
/// <remarks>
/// Thread-safe: Stateless class with readonly FareRule dependency. Pure delegation.
/// Supports concurrent calls from multiple threads.
/// </remarks>
public class MotorbikeFareStrategy : IFareCalculationStrategy
    {
        private readonly FareRule _fareRule;

        public MotorbikeFareStrategy(FareRule fareRule)
        {
            _fareRule = fareRule ?? throw new ArgumentNullException(nameof(fareRule));
        }

[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public Fare CalculateFare(double distanceKm)
        {
            return _fareRule.CalculateFare(distanceKm);
        }
    }
}