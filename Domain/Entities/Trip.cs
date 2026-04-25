using Domain.Enums;
using Domain.StateMachines;
using System;
using Domain.Events;
using Domain.ValueObjects;
using Domain.SharedKernel;

namespace Domain.Entities
{
    public class Trip : Entity
    {
        #region Fields
        private TripStatus _status;
        private readonly Guid _passengerId;
        private Guid? _driverId;
        private readonly VehicleType _tripVehicleType;
        // Composition
        private readonly Fare _tripFare;
        private readonly Route _tripRoute;
        private bool _isPaid;
        private readonly DateTime _requestAt;
        #endregion

        #region Properties
        public TripStatus Status
        {
            get => _status;
            private set => _status = value;
        }

      public Guid PassengerId => _passengerId;

        public Guid? DriverId => _driverId;
        // public int Version { get; private set; } = 1;
        public Route TripRoute => _tripRoute;
        public Fare TripFare => _tripFare;
        public double? Distance => _tripRoute?.Distance;
        public double? Duration => _tripRoute?.Duration.TotalSeconds;
        public bool IsPaid => _isPaid;
        public DateTime RequestAt => _requestAt;
        public VehicleType TripVehicleType => _tripVehicleType;
        #endregion

        #region Constructors
        /// <summary>
        /// Constructor cho business logic (tạo mới Trip).
        /// Auto-generate Id và CreatedAt.
        /// </summary>
        public Trip(Guid passengerId, Route route, Fare fare, VehicleType vehicleType)
            : base(Guid.NewGuid())
        {
            if (passengerId == Guid.Empty) throw new ArgumentException("Id không hợp lệ");
            
            _passengerId = passengerId;
            _tripRoute = route ?? throw new ArgumentNullException(nameof(route));
            _tripFare = fare ?? throw new ArgumentNullException(nameof(fare));
            if (vehicleType == 0) throw new ArgumentException("Loại xe không hợp lệ");
            _tripVehicleType = vehicleType;
            _status = TripStatus.Requested;
            _requestAt = DateTime.UtcNow;
            AddEvent(new TripRequestedEvent(Id, _passengerId, _tripRoute.Pickup, _tripRoute.Destination, _tripVehicleType));
            AddEvent(new TripSearchingEvent(Id));
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
        private void SetStatus(TripStatus newStatus)
        {
            if (!TripStateMachine.CanTransition(_status, newStatus))
            {
                throw new InvalidOperationException(nameof(Status), new Exception("Quy tắc nghiệp vụ: Chuyển đổi trạng thái chuyến đi không hợp lệ."));
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
            if (driverId == Guid.Empty) throw new ArgumentException("DriverId không hợp lệ", nameof(driverId));
            if (_driverId.HasValue)
                throw new Exception("Chuyến đi đã được gán tài xế.");

            _driverId = driverId;
            SetStatus(TripStatus.Matched);
            AddEvent(new TripMatchedEvent(Id, driverId));
        }

        public void MarkAsArrived()
        {
            if (!DriverId.HasValue)
               throw new Exception(nameof(DriverId), new Exception("Chuyến di chưa được gán tài xế."));

            SetStatus(TripStatus.Arrived);
            AddEvent(new TripArrivedEvent(Id));
        }

        public void StartTrip()
        {
            if (!DriverId.HasValue)
               throw new Exception(nameof(DriverId), new Exception("Chuyến di chưa được gán tài xế."));

            SetStatus(TripStatus.Started);
            AddEvent(new TripStartedEvent(Id));
        }
        public void CompleteTrip()
        {
            if (!DriverId.HasValue)
               throw new InvalidOperationException("Chuyến đi phải được gán tài xế trước khi hoàn thành.");

            SetStatus(TripStatus.Completed);

            AddEvent(new TripCompletedEvent(
                Id,
                PassengerId,
                DriverId.Value,
                _tripFare
            ));
        }

        public void ConfirmPayment()
        {
            if (_isPaid)
               throw new InvalidOperationException("Chuyến đã được thanh toán.");

            _isPaid = true;

            AddEvent(new TripPaidEvent(
                Id,
                PassengerId,
                DriverId.Value,
                _tripFare.TotalAmount
            ));
        }


        public void Cancel(string reason)
        {
            SetStatus(TripStatus.Cancelled);
            AddEvent(new TripCancelledEvent(Id, reason));
        }

        public void MarkTimeout()
        {
            SetStatus(TripStatus.Timeout);
            AddEvent(new TripTimeoutEvent(Id));
        }
        #endregion

    }
}
