namespace OOP2026
{
    public interface IDriverState
    {
        DriverStatus Status { get; }
        void SetOnline(Drv driver);
        void SetOnTrip(Drv driver);
        void SetOffline(Drv driver);
    }

    // --- Simple Factory cho Drv State ---
    public static class DriverStateFactory
    {
        public static IDriverState Create(DriverStatus status)
        {
            switch (status)
            {
                case DriverStatus.Offline: return new DriverOfflineState();
                case DriverStatus.Online: return new DriverOnlineState();
                case DriverStatus.OnTrip: return new DriverOnTripState();
                default: throw new ArgumentException("Trạng thái tài xế không hợp lệ.", nameof(status));
            }
        }
    }

    // --- Các Concrete States ---
    public class DriverOfflineState : IDriverState
    {
        public DriverStatus Status { get { return DriverStatus.Offline; } }

        public void SetOnline(Drv driver) { driver.TransitionTo(new DriverOnlineState()); }
        public void SetOffline(Drv driver) { throw new InvalidOperationException("Tài xế đã ở trạng thái Offline."); }
        public void SetOnTrip(Drv driver) { throw new InvalidOperationException("Tài xế đang Offline, không thể thực hiện chuyến đi."); }
    }
    public class DriverOnlineState : IDriverState
    {
        public DriverStatus Status { get { return DriverStatus.Online; } }

        public void SetOnline(Drv driver) { throw new InvalidOperationException("Tài xế đã ở trạng thái Online."); }
        public void SetOffline(Drv driver) { driver.TransitionTo(new DriverOfflineState()); }
        public void SetOnTrip(Drv driver) { driver.TransitionTo(new DriverOnTripState()); }
    }
    public class DriverOnTripState : IDriverState
    {
        public DriverStatus Status { get { return DriverStatus.OnTrip; } }

        public void SetOnline(Drv driver) { driver.TransitionTo(new DriverOnlineState()); }
        public void SetOffline(Drv driver) { throw new InvalidOperationException("Tài xế đang trong chuyến đi, không thể tắt ứng dụng."); }
        public void SetOnTrip(Drv driver) { throw new InvalidOperationException("Tài xế đã đang ở trong một chuyến đi khác."); }
    }

    // ---------- TRIP STATE ----------

    public interface ITripState
    {
        TripStatus Status { get; }

        void StartSearching(Trip trip);
        void AssignDriver(Trip trip, Guid driverId);
        void ArrivedPickup(Trip trip);
        void BeginTrip(Trip trip);
        void ArrivedDropoff(Trip trip);
        void ConfirmPayment(Trip trip);
        void Cancel(Trip trip, string reason);
        void Timeout(Trip trip);
    }
    // --- Simple Factory cho Trip State ---
    public static class TripStateFactory
    {
        public static ITripState Create(TripStatus status)
        {
            switch (status)
            {
                case TripStatus.Pending: return new TripPendingState();
                case TripStatus.Searching: return new TripSearchingState();
                case TripStatus.Matched: return new TripMatchedState();
                case TripStatus.Arrived: return new TripArrivedState();
                case TripStatus.Started: return new TripStartedState();
                case TripStatus.DropOff: return new TripDropOffState();
                case TripStatus.Completed: return new TripCompletedState();
                case TripStatus.Cancelled: return new TripCancelledState();
                case TripStatus.Timeout: return new TripTimeoutState();
                default: throw new ArgumentException("Trạng thái chuyến đi không hợp lệ.", nameof(status));
            }
        }
    }
    // --- Định nghĩa các Concrete States Base hành vi mặc định ---
    public abstract class AbstractTripState : ITripState
    {
        public abstract TripStatus Status { get; }
        protected void ChangeState(Trip trip, ITripState newState)
        {
            trip.TransitionTo(newState);
        }

        public virtual void StartSearching(Trip trip) { throw new InvalidOperationException("Không thể tìm tài xế ở trạng thái " + Status + "."); }
        public virtual void AssignDriver(Trip trip, Guid driverId) { throw new InvalidOperationException("Không thể gán tài xế ở trạng thái " + Status + "."); }
        public virtual void ArrivedPickup(Trip trip) { throw new InvalidOperationException("Không thể cập nhật 'Tài xế đã đến' ở trạng thái " + Status + "."); }
        public virtual void BeginTrip(Trip trip) { throw new InvalidOperationException("Không thể bắt đầu chuyến đi ở trạng thái " + Status + "."); }
        public virtual void ArrivedDropoff(Trip trip) { throw new InvalidOperationException("Không thể kết thúc chuyến đi ở trạng thái " + Status + "."); }
        public virtual void Cancel(Trip trip, string reason) { throw new InvalidOperationException("Không thể hủy chuyến đi ở trạng thái " + Status + "."); }
        public virtual void Timeout(Trip trip) { throw new InvalidOperationException("Chuyến đi không thể kích hoạt hết giờ ở trạng thái " + Status + "."); }
        public virtual void ConfirmPayment(Trip trip) { throw new InvalidOperationException("Không thể xác nhận thanh toán ở trạng thái " + Status + "."); }
    }
    // --- Các Trạng thái cụ thể triển khai luật Nghiệp vụ ---
    public class TripPendingState : AbstractTripState
    {
        public override TripStatus Status { get { return TripStatus.Pending; } }
        public override void StartSearching(Trip trip) { ChangeState(trip, new TripSearchingState()); }
        public override void Cancel(Trip trip, string reason) { ChangeState(trip, new TripCancelledState()); }
    }
    public class TripSearchingState : AbstractTripState
    {
        public override TripStatus Status { get { return TripStatus.Searching; } }
        public override void AssignDriver(Trip trip, Guid driverId)
        {
            trip.SetDriverId(driverId);
            ChangeState(trip, new TripMatchedState());
        }
        public override void Cancel(Trip trip, string reason) { ChangeState(trip, new TripCancelledState()); }
        public override void Timeout(Trip trip) { ChangeState(trip, new TripTimeoutState()); }
    }

    public class TripMatchedState : AbstractTripState
    {
        public override TripStatus Status { get { return TripStatus.Matched; } }
        public override void ArrivedPickup(Trip trip) { ChangeState(trip, new TripArrivedState()); }
        public override void Cancel(Trip trip, string reason) { ChangeState(trip, new TripCancelledState()); }
    }

    public class TripArrivedState : AbstractTripState
    {
        public override TripStatus Status { get { return TripStatus.Arrived; } }
        public override void BeginTrip(Trip trip) { ChangeState(trip, new TripStartedState()); }
        public override void Cancel(Trip trip, string reason) { ChangeState(trip, new TripCancelledState()); }
    }
    public class TripStartedState : AbstractTripState
    {
        public override TripStatus Status { get { return TripStatus.Started; } }
        public override void ArrivedDropoff(Trip trip) { ChangeState(trip, new TripDropOffState()); }
    }

    public class TripDropOffState : AbstractTripState
    {
        public override TripStatus Status { get { return TripStatus.DropOff; } }
        public override void ConfirmPayment(Trip trip)
        {
            trip.MarkPaymentAsSettled();
            ChangeState(trip, new TripCompletedState());
        }
    }

    public class TripCompletedState : AbstractTripState
    {
        public override TripStatus Status { get { return TripStatus.Completed; } }
    }

    public class TripCancelledState : AbstractTripState
    {
        public override TripStatus Status { get { return TripStatus.Cancelled; } }
    }

    public class TripTimeoutState : AbstractTripState
    {
        public override TripStatus Status { get { return TripStatus.Timeout; } }
    }
    // EventArgs cho Drv Observer
    public class DriverStatusChangedEventArgs : EventArgs
    {
        private Guid _driverId;
        private DriverStatus _oldStatus;
        private DriverStatus _newStatus;

        public Guid DriverId { get { return _driverId; } }
        public DriverStatus OldStatus { get { return _oldStatus; } }
        public DriverStatus NewStatus { get { return _newStatus; } }

        public DriverStatusChangedEventArgs(Guid driverId, DriverStatus oldStatus, DriverStatus newStatus)
        {
            _driverId = driverId;
            _oldStatus = oldStatus;
            _newStatus = newStatus;
        }
    }
    // EventArgs cho Trip Observer
    public class TripStatusChangedEventArgs : EventArgs
    {
        private Guid _tripId;
        private TripStatus _oldStatus;
        private TripStatus _newStatus;

        public Guid TripId { get { return _tripId; } }
        public TripStatus OldStatus { get { return _oldStatus; } }
        public TripStatus NewStatus { get { return _newStatus; } }

        public TripStatusChangedEventArgs(Guid tripId, TripStatus oldStatus, TripStatus newStatus)
        {
            _tripId = tripId;
            _oldStatus = oldStatus;
            _newStatus = newStatus;
        }
    }
    public static class UserFactory
    {
        public static Usr CreateAdmin(string name, string phone, string password)
        {
            return new Adm(name, phone, password);
        }
        public static Usr CreatePassenger(string name, string phone, string password)
        {
            return new Psg(name, phone, password);
        }
        public static Usr CreateDriver(string name, string phone, string password, string licenseNumber, Guid vehicleId, Loc position)
        {
            return new Drv(name, phone, password, licenseNumber, vehicleId, position);
        }
    }

    public static class VehicleFactory
    {
        public static Veh CreateVehicle(VehicleType type, string plateNumber, string brand, string model, string color, int capacity)
        {
            switch (type)
            {
                case VehicleType.Car: return new Car(plateNumber, brand, model, color, capacity);
                case VehicleType.Moto: return new Moto(plateNumber, brand, model, color, capacity);
                default: throw new ArgumentException("Loại phương tiện không hợp lệ.", nameof(type));
            }
        }
    }
}
