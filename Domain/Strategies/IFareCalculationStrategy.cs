using Domain.ValueObjects;

namespace Domain.Strategies
{
    /// <summary>
    /// Strategy interface for fare calculation.
    /// </summary>
    /// <remarks>
    /// Each concrete strategy encapsulates the fare calculation algorithm for a specific vehicle type.
    /// This follows the Strategy Pattern described in docs/Strategy.md.
    /// </remarks>
    public interface IFareCalculationStrategy
    {
/// <summary>
/// Calculates the fare for a given distance.
/// </summary>
/// <param name="distanceKm">The distance in kilometers.</param>
/// <returns>A <see cref="Fare"/> object containing total amount, commission, and driver income.</returns>
/// <remarks>
/// Thread-safe: Pure function with no shared mutable state. Supports concurrent/multithreaded calls
/// (e.g., parallel simulations, multiple trip calculations). Immutable Fare result.
/// </remarks>
[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
Fare CalculateFare(double distanceKm);
    }
}
