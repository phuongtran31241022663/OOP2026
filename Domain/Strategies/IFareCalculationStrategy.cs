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
        /// hình như method này nên hoạt động đa luồng
        Fare CalculateFare(double distanceKm);
    }
}
