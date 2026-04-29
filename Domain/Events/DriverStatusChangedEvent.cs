using System;
using Domain.SharedKernel;

namespace Domain.Events
{
    /// <summary>
    /// Event được kích hoạt khi trạng thái của tài xế thay đổi.
    /// </summary>
    public class DriverStatusChangedEvent : DomainEvent
    {
        #region Properties

        /// <summary>
        /// ID của tài xế.
        /// </summary>
        public Guid DriverId { get; }

        /// <summary>
        /// Trạng thái cũ của tài xế.
        /// </summary>
        public string OldStatus { get; }

        /// <summary>
        /// Trạng thái mới của tài xế.
        /// </summary>
        public string NewStatus { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Khởi tạo một instance của <see cref="DriverStatusChangedEvent"/>.
        /// </summary>
        /// <param name="driverId">ID của tài xế.</param>
        /// <param name="oldStatus">Trạng thái cũ.</param>
        /// <param name="newStatus">Trạng thái mới.</param>
        public DriverStatusChangedEvent(Guid driverId, string oldStatus, string newStatus)
        {
            DriverId = driverId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }

        #endregion
    }
}
