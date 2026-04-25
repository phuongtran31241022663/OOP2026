using Domain.Enums;
using Domain.Events;
using Domain.SharedKernel;
using Domain.States;
using Domain.ValueObjects;
using System;

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
        private ITripState _currentState;
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
            _currentState = new RequestedState();
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

        #region State Pattern helpers (internal)
        internal void TransitionTo(ITripState newState)
        {
            _currentState = newState;
        }
        internal void SetStatusInternal(TripStatus newStatus)
        {
            _status = newStatus;
        }

        internal void SetDriverId(Guid driverId)
        {
            _driverId = driverId;
        }

        #endregion

        #region Public behavior (delegated to state)
        public void SetSearching()
        {
            _currentState.SetSearching(this);
        }

        public void MatchDriver(Guid driverId)
        {
            _currentState.MatchDriver(this, driverId);
        }

        public void MarkAsArrived()
        {
            _currentState.MarkAsArrived(this);
        }
        public void StartTrip()
        {
            _currentState.StartTrip(this);
        }

        public void CompleteTrip()
        {
            _currentState.CompleteTrip(this);
        }

        public void Cancel(string reason)
        {
            _currentState.Cancel(this, reason);
        }

        public void MarkTimeout()
        {
            _currentState.MarkTimeout(this);
        }
        #endregion

        #region Payment
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


        #endregion

    }
}
