using Domain.Enums;
using Domain.StateMachines;
using System;
using Domain.Trips.Events;
using Domain.ValueObjects;
using Domain.SharedKernel;

namespace Domain.Trips
{
    public class Trip : Entity
    {
        #region Fields
        private TripStatus _status;
        private Guid _passengerId;
        private Guid? _driverId;
        private readonly VehicleType _vehicleType;
        // Composition
        private readonly Fare _fare;
        private readonly Route _route;
        private bool _isPaid;
        private readonly DateTime _requestAt;
        #endregion

        #region Properties
        public TripStatus Status
        {
            get => _status;
            private set => _status = value;
        }

        public Guid PassengerId
        {
            get => _passengerId;
            private set
            {
                if (value == Guid.Empty)
                    throw new Exception(nameof(PassengerId), new Exception("PassengerId không hợp lệ."));
                _passengerId = value;
            }
        }
        public Guid? DriverId
        {
            get => _driverId;
            private set
            {
                if (value.HasValue && value.Value == Guid.Empty)
                    throw new Exception(nameof(DriverId), new Exception("DriverId không hợp lệ."));
                _driverId = value;
            }
        }
        // public int Version { get; private set; } = 1;
        public Route Route => _route;
        public Fare Fare => _fare;
        public double? Distance => _route?.Distance;
        public double? Duration => _route?.Duration.TotalSeconds;
        public bool IsPaid => _isPaid;
        public DateTime RequestAt => _requestAt;

        public VehicleType VehicleType => _vehicleType;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor cho business logic (tạo mới Trip).
        /// Auto-generate Id và CreatedAt.
        /// </summary>
        public Trip(Guid id, Guid passengerId, Guid? driverId, VehicleType vehicleType,
            Fare fare, Route route, bool isPaid) : base(id)
        {
            _route = route ?? throw new ArgumentNullException(nameof(route));
            PassengerId = passengerId;
            DriverId = driverId;
            _vehicleType = vehicleType;
            Status = TripStatus.Requested;
            _isPaid = false;
            _requestAt = DateTime.UtcNow;

            AddEvent(new TripRequestedEvent(Id, passengerId, _route.Pickup, _route.Destination, _vehicleType));
        }

        /// <summary>
        /// Constructor cho persistence (ORM/JSON).
        /// Private parameterless để ORM có thể instantiate, sau đó set properties.
        /// </summary>
        private Trip() : base(default)
        {
        }
        #endregion

        #region State Management
        public void SetStatus(TripStatus newStatus)
        {
            if (!TripStateMachine.CanTransition(_status, newStatus))
            {
                throw new Exception(nameof(Status), new Exception("Quy tắc nghiệp vụ: Chuyển đổi trạng thái chuyến đi không hợp lệ."));
            }
            _status = newStatus;
        }   

        public void SetSearching()
        {
            SetStatus(TripStatus.Searching);
            AddEvent(new TripSearchingEvent(Id));
        }

        public void MatchDriver(Guid driverId)
        {
            //if (DriverId.HasValue)
            //    throw new Exception(nameof(DriverId), new Exception("Chuyến di chưa được gán tài xế."));

            DriverId = driverId;
            SetStatus(TripStatus.Matched);
            AddEvent(new TripMatchedEvent(Id, driverId, _vehicleType));
        }

        public void MarkAsArrived()
        {
            //if (!DriverId.HasValue)
            //    throw new Exception(nameof(DriverId), new Exception("Chuyến di chưa được gán tài xế."));

            SetStatus(TripStatus.Arrived);
            AddEvent(new TripArrivedEvent(Id));
        }

        public void StartTrip()
        {
            //if (!DriverId.HasValue)
            //    throw new Exception(nameof(DriverId), new Exception("Chuyến di chưa được gán tài xế."));

            SetStatus(TripStatus.Started);
            AddEvent(new TripStartedEvent(Id));
        }
        public void CompleteTrip()
        {
            //if (Status != TripStatus.Started)
            //    throw new InvalidOperationException("Chuyến đi phải đang tiến hành.");

            //if (!DriverId.HasValue)
            //    throw new InvalidOperationException("Chuyến đi phải được gán tài xế trước khi hoàn thành.");

            SetStatus(TripStatus.Completed);

            AddEvent(new TripCompletedEvent(
                Id,
                PassengerId,
                DriverId.Value,
                _fare
            ));
        }

        public void ConfirmPayment()
        {
            //if (Status != TripStatus.Completed)
            //    throw new InvalidOperationException("Chỉ có thể xác nhận thanh toán khi chuyến đã hoàn thành.");

            //if (_isPaid)
            //    throw new InvalidOperationException("Chuyến đã được thanh toán.");

            _isPaid = true;

            AddEvent(new TripPaidEvent(
                Id,
                PassengerId,
                DriverId.Value,
                _fare.TotalAmount
            ));
        }


        public void Cancel(string reason)
        {
            //if (Status == TripStatus.Completed)
            //    throw new Exception(nameof(Status), new Exception("Không thể hủy chuyến đã hoàn thành."));

            SetStatus(TripStatus.Cancelled);
            AddEvent(new TripCancelledEvent(Id, reason));
        }

        public void MarkTimeout()
        {
            //if (Status != TripStatus.Searching)
            //    throw new Exception(nameof(Status), new Exception("Chỉ có thể timeout khi đang tìm tài xế."));

            SetStatus(TripStatus.Timeout);
            AddEvent(new TripTimeoutEvent(Id));
        }
        #endregion

    }
}
