using Domain.Entities;
using Domain.ValueObjects;
using System;

namespace Domain.Strategies
{
/// <summary>
/// Concrete strategy for car fare calculation.
/// </summary>
/// <remarks>
/// Thread-safe: Stateless class with readonly FareRule dependency. Pure delegation.
/// Supports concurrent calls from multiple threads.
/// </remarks>
public class CarFareStrategy : IFareCalculationStrategy
    {
        private readonly FareRule _fareRule;

        public CarFareStrategy(FareRule fareRule)
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