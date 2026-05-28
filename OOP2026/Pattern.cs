namespace OOP2026
{
    /*
     * =====================================================================
     * STATE PATTERN cho Drv và Trip
     *
     * Vai trò trong pattern:
     * - Context      : Drv, Trip (chứa state hiện tại, ủy thác hành vi)
     * - State        : IDriverState, ITripState (giao diện chung cho các state)
     * - ConcreteState: DriverOnlineState, DriverOfflineState, ...
     *                  PendingState, SearchingState, MatchedState, ...
     *
     * Mỗi ConcreteState tự quyết định khi nào chuyển sang state khác
     * bằng cách gọi context.TransitionTo(...).
     * =====================================================================
     */
    /*
 * =====================================================================
 * OBSERVER PATTERN – Triển khai theo mẫu Microsoft Learn
 *
 * Provider (Drv, Trip)  ->  phát thông báo qua event
 * Observer mới (DriverStatusMonitor, TripStatusMonitor) triển khai
 * IObserver<EventArgs tương ứng> và đăng ký qua Subscribe/Unsubscribe.
 *
 * =====================================================================
 */
    // ---------- DRIVER STATE ----------

    /// <summary>
    /// State: Định nghĩa các hành vi phụ thuộc trạng thái của Drv.
    /// </summary>
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
            return status switch
            {
                DriverStatus.Offline => new DriverOfflineState(),
                DriverStatus.Online => new DriverOnlineState(),
                DriverStatus.OnTrip => new DriverOnTripState(),
                _ => throw new ArgumentException("Trạng thái tài xế không hợp lệ.", nameof(status))
            };
        }
    }

    // --- Các Concrete States ---
    public class DriverOfflineState : IDriverState
    {
        public DriverStatus Status => DriverStatus.Offline;

        public void SetOnline(Drv driver) => driver.TransitionTo(new DriverOnlineState());
        public void SetOffline(Drv driver) => throw new InvalidOperationException("Tài xế đã ở trạng thái Offline.");
        public void SetOnTrip(Drv driver) => throw new InvalidOperationException("Tài xế đang Offline, không thể thực hiện chuyến đi.");
    }
    public class DriverOnlineState : IDriverState
    {
        public DriverStatus Status => DriverStatus.Online;

        public void SetOnline(Drv driver) => throw new InvalidOperationException("Tài xế đã ở trạng thái Online.");
        public void SetOffline(Drv driver) => driver.TransitionTo(new DriverOfflineState());
        public void SetOnTrip(Drv driver) => driver.TransitionTo(new DriverOnTripState());
    }
    public class DriverOnTripState : IDriverState
    {
        public DriverStatus Status => DriverStatus.OnTrip;

        public void SetOnline(Drv driver) => throw new InvalidOperationException("Tài xế đang trong chuyến đi, không thể bật Online.");
        public void SetOffline(Drv driver) => throw new InvalidOperationException("Tài xế đang trong chuyến đi, không thể tắt ứng dụng.");
        public void SetOnTrip(Drv driver) => throw new InvalidOperationException("Tài xế đã đang ở trong một chuyến đi khác.");
    }

    // ---------- TRIP STATE ----------

    /// <summary>
    /// State: Định nghĩa các hành vi phụ thuộc trạng thái của Trip.
    /// </summary>
    public interface ITripState
    {
        TripStatus Status { get; }

        void StartSearching(Trip trip);
        void AssignDriver(Trip trip, Guid driverId);
        void DriverArrived(Trip trip);
        void BeginTrip(Trip trip);
        void FinishTrip(Trip trip);
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
        protected void ChangeState(Trip trip, ITripState newState) => trip.TransitionTo(newState);

        public virtual void StartSearching(Trip trip) => throw new InvalidOperationException($"Không thể tìm tài xế ở trạng thái {Status}.");
        public virtual void AssignDriver(Trip trip, Guid driverId) => throw new InvalidOperationException($"Không thể gán tài xế ở trạng thái {Status}.");
        public virtual void DriverArrived(Trip trip) => throw new InvalidOperationException($"Không thể cập nhật 'Tài xế đã đến' ở trạng thái {Status}.");
        public virtual void BeginTrip(Trip trip) => throw new InvalidOperationException($"Không thể bắt đầu chuyến đi ở trạng thái {Status}.");
        public virtual void FinishTrip(Trip trip) => throw new InvalidOperationException($"Không thể kết thúc chuyến đi ở trạng thái {Status}.");
        public virtual void Cancel(Trip trip, string reason) => throw new InvalidOperationException($"Không thể hủy chuyến đi ở trạng thái {Status}.");
        public virtual void Timeout(Trip trip) => throw new InvalidOperationException($"Chuyến đi không thể kích hoạt hết giờ ở trạng thái {Status}.");
        public virtual void ConfirmPayment(Trip trip) => throw new InvalidOperationException($"Không thể xác nhận thanh toán ở trạng thái {Status}.");
    }
    // --- Các Trạng thái cụ thể triển khai luật Nghiệp vụ ---
    public class TripPendingState : AbstractTripState
    {
        public override TripStatus Status => TripStatus.Pending;
        public override void StartSearching(Trip trip) => ChangeState(trip, new TripSearchingState());
        public override void Cancel(Trip trip, string reason) => ChangeState(trip, new TripCancelledState());
    }
    public class TripSearchingState : AbstractTripState
    {
        public override TripStatus Status => TripStatus.Searching;
        public override void AssignDriver(Trip trip, Guid driverId)
        {
            trip.SetDriverId(driverId);
            ChangeState(trip, new TripMatchedState());
        }
        public override void Cancel(Trip trip, string reason) => ChangeState(trip, new TripCancelledState());
        public override void Timeout(Trip trip) => ChangeState(trip, new TripTimeoutState());
    }

    public class TripMatchedState : AbstractTripState
    {
        public override TripStatus Status => TripStatus.Matched;
        public override void DriverArrived(Trip trip) => ChangeState(trip, new TripArrivedState());
        public override void Cancel(Trip trip, string reason) => ChangeState(trip, new TripCancelledState());
    }

    public class TripArrivedState : AbstractTripState
    {
        public override TripStatus Status => TripStatus.Arrived;
        public override void BeginTrip(Trip trip) => ChangeState(trip, new TripStartedState());
        public override void Cancel(Trip trip, string reason) => ChangeState(trip, new TripCancelledState());
    }
    public class TripStartedState : AbstractTripState
    {
        public override TripStatus Status => TripStatus.Started;
        public override void FinishTrip(Trip trip) => ChangeState(trip, new TripDropOffState());
    }

    public class TripDropOffState : AbstractTripState
    {
        public override TripStatus Status => TripStatus.DropOff;
        public override void ConfirmPayment(Trip trip)
        {
            trip.MarkPaymentAsSettled();
            ChangeState(trip, new TripCompletedState());
        }
    }

    public class TripCompletedState : AbstractTripState
    {
        public override TripStatus Status => TripStatus.Completed;
    }

    public class TripCancelledState : AbstractTripState
    {
        public override TripStatus Status => TripStatus.Cancelled;
    }

    public class TripTimeoutState : AbstractTripState
    {
        public override TripStatus Status => TripStatus.Timeout;
    }
    // EventArgs cho Drv Observer
    public class DriverStatusChangedEventArgs : EventArgs
    {
        public Guid DriverId { get; }
        public DriverStatus OldStatus { get; }
        public DriverStatus NewStatus { get; }

        public DriverStatusChangedEventArgs(Guid driverId, DriverStatus oldStatus, DriverStatus newStatus)
        {
            DriverId = driverId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }
    }
    // EventArgs cho Trip Observer
    public class TripStatusChangedEventArgs : EventArgs
    {
        public Guid TripId { get; }
        public TripStatus OldStatus { get; }
        public TripStatus NewStatus { get; }
        public TripStatusChangedEventArgs(Guid tripId, TripStatus oldStatus, TripStatus newStatus)
        {
            TripId = tripId;
            OldStatus = oldStatus;
            NewStatus = newStatus;
        }
    }
    public abstract class StatusMonitorBase<TEventArgs> : IObserver<TEventArgs>
    {
        protected readonly string MonitorName;
        // Chuyển sang danh sách để quản lý đa đăng ký (Multi-subscription)
        private readonly List<IDisposable> _unsubscribers = new List<IDisposable>();
        private readonly List<string> _log = new List<string>();

        protected StatusMonitorBase(string name)
        {
            MonitorName = string.IsNullOrWhiteSpace(name) ? "Monitor" : name;
        }

        /// <summary>
        /// Thêm một kết nối theo dõi mới vào hệ thống mà không làm mất các kết nối cũ.
        /// </summary>
        protected void SubscribeTo(IObservable<TEventArgs> observable)
        {
            if (observable == null) return;

            IDisposable subscription = observable.Subscribe(this);
            _unsubscribers.Add(subscription);
        }

        /// <summary>
        /// Ngắt toàn bộ các đối tượng đang theo dõi và dọn dẹp log.
        /// </summary>
        public virtual void Unsubscribe()
        {
            foreach (var unsubscriber in _unsubscribers)
            {
                unsubscriber?.Dispose();
            }
            _unsubscribers.Clear();
            _log.Clear();
        }

        public virtual void OnCompleted()
        {
            // Kết thúc giám sát nhưng giữ lại lịch sử log để kiểm tra nếu cần, hoặc xóa tùy nghiệp vụ
            Console.WriteLine($"[{MonitorName}] Tiến trình theo dõi của một hoặc các luồng đã kết thúc.");
        }

        public virtual void OnError(Exception error)
        {
            Console.WriteLine($"[{MonitorName}] Gặp lỗi hệ thống: {error.Message}");
        }

        public void OnNext(TEventArgs value)
        {
            string msg = FormatMessage(value);
            _log.Add(msg);
            Console.WriteLine(msg);
        }

        public IReadOnlyList<string> Log => _log.AsReadOnly();

        protected abstract string FormatMessage(TEventArgs args);
    }
    public class DriverStatusMonitor : StatusMonitorBase<DriverStatusChangedEventArgs>
    {
        public DriverStatusMonitor(string name) : base(name) { }

        public void Subscribe(Drv driver)
        {
            if (driver == null) throw new ArgumentNullException(nameof(driver));

            var observable = new EventObservable<DriverStatusChangedEventArgs>(
                handler => driver.StatusChanged += handler,
                handler => driver.StatusChanged -= handler);

            SubscribeTo(observable);
        }

        protected override string FormatMessage(DriverStatusChangedEventArgs args)
        {
            return $"[{MonitorName}] Drv {args.DriverId}: {args.OldStatus} -> {args.NewStatus}";
        }
    }

    public class TripStatusMonitor : StatusMonitorBase<TripStatusChangedEventArgs>
    {
        public TripStatusMonitor(string name) : base(name) { }

        public void Subscribe(Trip trip)
        {
            if (trip == null) throw new ArgumentNullException(nameof(trip));

            var observable = new EventObservable<TripStatusChangedEventArgs>(
                handler => trip.StatusChanged += handler,
                handler => trip.StatusChanged -= handler);

            SubscribeTo(observable);
        }

        protected override string FormatMessage(TripStatusChangedEventArgs args)
        {
            return $"[{MonitorName}] Trip {args.TripId}: {args.OldStatus} -> {args.NewStatus}";
        }
    }
    /// <summary>
    /// Helper: Chuyển đổi event C# thành IObservable<T> để tương thích với IObserver<T>.
    /// (Không cần dùng thư viện ngoài, chỉ dùng delegate đơn giản)
    /// </summary>
    internal sealed class EventObservable<T> : IObservable<T>
    {
        private readonly Action<EventHandler<T>> _addHandler;
        private readonly Action<EventHandler<T>> _removeHandler;

        public EventObservable(Action<EventHandler<T>> add, Action<EventHandler<T>> remove)
        {
            _addHandler = add;
            _removeHandler = remove;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            EventHandler<T> handler = (sender, args) => observer.OnNext(args);
            _addHandler(handler);
            return new Unsubscriber(() => _removeHandler(handler));
        }

        private sealed class Unsubscriber : IDisposable
        {
            private readonly Action _unsubscribe;
            public Unsubscriber(Action unsubscribe) => _unsubscribe = unsubscribe;
            public void Dispose() => _unsubscribe();
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
