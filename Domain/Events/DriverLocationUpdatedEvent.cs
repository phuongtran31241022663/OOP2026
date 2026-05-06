using Domain.SharedKernel;
using Domain.ValueObjects;
using System;

namespace Domain.Events
{
    /// <summary>
    /// Event được kích hoạt khi vị trí của tài xế được cập nhật.
    /// </summary>
    public class DriverLocationUpdatedEvent : DomainEvent
    {
        #region Properties

        /// <summary>
        /// ID của tài xế.
        /// </summary>
        public Guid DriverId { get; }

        /// <summary>
        /// Vị trí mới của tài xế.
        /// </summary>
        public Location NewLocation { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Khởi tạo một instance của <see cref="DriverLocationUpdatedEvent"/>.
        /// </summary>
        /// <param name="driverId">ID của tài xế.</param>
        /// <param name="newLocation">Vị trí mới.</param>
        public DriverLocationUpdatedEvent(Guid driverId, Location newLocation)
        {
            DriverId = driverId;
            NewLocation = newLocation;
        }

        #endregion
    }
}