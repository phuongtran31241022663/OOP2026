using System;
using Domain.SharedKernel;
using Domain.ValueObjects;
using Domain.Enums;

namespace Domain.Events
{
    /// <summary>
    /// Event được kích hoạt khi một hành khách yêu cầu một chuyến đi mới.
    /// </summary>
    public class TripRequestedEvent : DomainEvent
    {
        #region Properties

        /// <summary>
        /// ID của chuyến đi vừa được tạo.
        /// </summary>
        public Guid TripId { get; }

        /// <summary>
        /// ID của hành khách yêu cầu chuyến đi.
        /// </summary>
        public Guid PassengerId { get; }

        /// <summary>
        /// Vị trí đón của chuyến đi.
        /// </summary>
        public Location Pickup { get; }

        /// <summary>
        /// Vị trí đích của chuyến đi.
        /// </summary>
        public Location Destination { get; }

        /// <summary>
        /// Loại phương tiện được yêu cầu.
        /// </summary>
        public VehicleType VehicleType { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Khởi tạo một instance của <see cref="TripRequestedEvent"/>.
        /// </summary>
        /// <param name="tripId">ID của chuyến đi.</param>
        /// <param name="passengerId">ID của hành khách.</param>
        /// <param name="pickup">Vị trí đón.</param>
        /// <param name="destination">Vị trí đích.</param>
        /// <param name="vehicleType">Loại phương tiện.</param>
        public TripRequestedEvent(
            Guid tripId,
            Guid passengerId,
            Location pickup,
            Location destination,
            VehicleType vehicleType)
        {
            TripId = tripId;
            PassengerId = passengerId;
            Pickup = pickup;
            Destination = destination;
            VehicleType = vehicleType;
        }

        #endregion
    }
}
