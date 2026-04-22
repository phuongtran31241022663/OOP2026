using Domain.ValueObjects;
using Domain.Trips;
using System;

namespace Application.Interfaces
{
    /// <summary>
    /// Service for simulating driver movement along routes in real-time.
    /// </summary>
    public interface IDriverSimulationService
    {
        /// <summary>
        /// Starts simulating a driver's movement along a route.
        /// </summary>
        /// <param name="tripId">The trip ID (used as simulation key)</param>
        /// <param name="driverId">The driver ID</param>
        /// <param name="route">The route to follow</param>
        void StartDriverSimulation(Guid tripId, Guid driverId, Route route);

        /// <summary>
        /// Stops the simulation for a specific trip.
        /// </summary>
        /// <param name="tripId">The trip ID</param>
        void StopDriverSimulation(Guid tripId);

        /// <summary>
        /// Gets the current position of a driver.
        /// </summary>
        /// <param name="driverId">The driver ID</param>
        /// <returns>The driver's current location, or null if not found</returns>
        Location GetDriverPosition(Guid driverId);

        /// <summary>
        /// Event raised when a driver's position changes during simulation.
        /// </summary>
        event Action<Guid, Location> DriverPositionChanged;
    }
}
