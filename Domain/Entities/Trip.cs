using Domain.Enums;
using Domain.Events;
using Domain.SharedKernel;
using Domain.States;
using Domain.ValueObjects;
using System;
using System.Threading;

namespace Domain.Entities
{
    /// <summary>
    /// Đại diện cho một chuyến đi trong hệ thống, là một Aggregate Root.
    /// </summary>
    /// <remarks>
    /// Lớp này quản lý toàn bộ vòng đời của một chuyến đi, từ lúc được yêu cầu cho đến khi hoàn thành hoặc bị hủy.
    /// <para>Nó sử dụng State Pattern để quản lý các trạng thái và các hành vi chuyển đổi giữa chúng.
    /// Các trạng thái hợp lệ bao gồm: Requested, Searching, Matched, Arrived, Started, Completed, Cancelled, Timeout.</para>
    /// <para>Quan hệ: Composition với <see cref="Route"/> và <see cref="Fare"/>.</para>
    /// </remarks>
    public class Trip : Entity
    {
        #region Fields

        private Guid _passengerId;
        private VehicleType _tripVehicleType;
        private Fare _tripFare;
        private Route _tripRoute;
        private DateTime _requestAt;

        private Guid? _driverId;
        private bool _isPaid;
        private ITripState _currentState;

        #endregion

        #region Properties

        /// <summary>
        /// Lấy trạng thái hiện tại của chuyến đi dưới dạng chuỗi (ví dụ: "Searching", "Completed").
        /// </summary>
        /// <remarks>
        /// Trạng thái được suy ra từ kiểu của đối tượng state hiện tại (<see cref="ITripState"/>).
        /// </remarks>
        public string Status
        {
            get
            {
                if (_currentState == null)
                    return "Requested";
                string name = _currentState.GetType().Name;
                if (name.EndsWith("State"))
                    name = name.Substring(0, name.Length - 5);
                return name;
            }
            private set
            {
                _currentState = value switch
                {
                    "Searching" => new SearchingState(),
                    "Matched" => new MatchedState(),
                    "Arrived" => new ArrivedState(),
                    "Started" => new StartedState(),
                    "Completed" => new CompletedState(),
                    "Cancelled" => new CancelledState(),
                    "Timeout" => new TimeoutState(),
                    _ => new RequestedState()
                };
            }
        }

        /// <summary>
        /// ID của hành khách đặt chuyến.
        /// </summary>
        public Guid PassengerId { get => _passengerId; private set => _passengerId = value; }

        /// <summary>
        /// ID của tài xế được ghép cặp. Có thể là <c>null</c> nếu chưa có tài xế.
        /// </summary>
        public Guid? DriverId => _driverId;

        /// <summary>
        /// Thông tin lộ trình của chuyến đi (điểm đón, điểm đến, khoảng cách, thời gian).
        /// </summary>
        public Route TripRoute { get => _tripRoute; private set => _tripRoute = value; }

        /// <summary>
        /// Thông tin giá cước của chuyến đi (tổng tiền, hoa hồng, thu nhập tài xế).
        /// </summary>
        public Fare TripFare { get => _tripFare; private set => _tripFare = value; }

        /// <summary>
        /// Khoảng cách của chuyến đi (tính bằng km), lấy từ <see cref="TripRoute"/>.
        /// </summary>
        public double? Distance => _tripRoute?.Distance;

        /// <summary>
        /// Thời gian dự kiến của chuyến đi (tính bằng giây), lấy từ <see cref="TripRoute"/>.
        /// </summary>
        public double? Duration => _tripRoute?.Duration.TotalSeconds;

        /// <summary>
        /// Cho biết chuyến đi đã được thanh toán hay chưa.
        /// </summary>
        public bool IsPaid => _isPaid;

        /// <summary>
        /// Thời điểm chuyến đi được yêu cầu (UTC).
        /// </summary>
        public DateTime RequestAt { get => _requestAt; private set => _requestAt = value; }

        /// <summary>
        /// Loại phương tiện được yêu cầu cho chuyến đi.
        /// </summary>
        public VehicleType TripVehicleType { get => _tripVehicleType; private set => _tripVehicleType = value; }

        #endregion

        #region State Checkers

        /// <summary>
        /// Kiểm tra xem chuyến đi có đang ở trạng thái tìm tài xế hay không.
        /// </summary>
        /// <returns><c>true</c> nếu trạng thái là <see cref="SearchingState"/>.</returns>
        public bool IsSearching() => _currentState is SearchingState;

        /// <summary>
        /// Kiểm tra xem chuyến đi đã được ghép cặp với tài xế hay không.
        /// </summary>
        /// <returns><c>true</c> nếu trạng thái là <see cref="MatchedState"/>.</returns>
        public bool IsMatched() => _currentState is MatchedState;

        /// <summary>
        /// Kiểm tra xem tài xế đã đến điểm đón hay không.
        /// </summary>
        /// <returns><c>true</c> nếu trạng thái là <see cref="ArrivedState"/>.</returns>
        public bool IsArrived() => _currentState is ArrivedState;

        /// <summary>
        /// Kiểm tra xem chuyến đi đã bắt đầu hay chưa.
        /// </summary>
        /// <returns><c>true</c> nếu trạng thái là <see cref="StartedState"/>.</returns>
        public bool IsStarted() => _currentState is StartedState;

        /// <summary>
        /// Kiểm tra xem chuyến đi đã hoàn thành hay chưa.
        /// </summary>
        /// <returns><c>true</c> nếu trạng thái là <see cref="CompletedState"/>.</returns>
        public bool IsCompleted() => _currentState is CompletedState;

        /// <summary>
        /// Kiểm tra xem chuyến đi đã bị hủy hay chưa.
        /// </summary>
        /// <returns><c>true</c> nếu trạng thái là <see cref="CancelledState"/>.</returns>
        public bool IsCancelled() => _currentState is CancelledState;

        /// <summary>
        /// Kiểm tra xem chuyến đi có bị hết thời gian chờ hay không.
        /// </summary>
        /// <returns><c>true</c> nếu trạng thái là <see cref="TimeoutState"/>.</returns>
        public bool IsTimeout() => _currentState is TimeoutState;

        /// <summary>
        /// Kiểm tra xem chuyến đi có ở trạng thái kết thúc (không thể thay đổi) hay không.
        /// </summary>
        /// <returns><c>true</c> nếu chuyến đi đã Hoàn thành, Bị hủy, hoặc Hết thời gian chờ.</returns>
        public bool IsTerminal() => IsCompleted() || IsCancelled() || IsTimeout();

        #endregion

        #region Constructors

        /// <summary>
        /// Khởi tạo một chuyến đi mới.
        /// </summary>
        /// <param name="passengerId">ID của hành khách.</param>
        /// <param name="route">Lộ trình của chuyến đi.</param>
        /// <param name="fare">Giá cước của chuyến đi.</param>
        /// <param name="vehicleType">Loại phương tiện yêu cầu.</param>
        /// <exception cref="ArgumentException">Ném khi <paramref name="passengerId"/> hoặc <paramref name="vehicleType"/> không hợp lệ.</exception>
        /// <exception cref="ArgumentNullException">Ném khi <paramref name="route"/> hoặc <paramref name="fare"/> là <c>null</c>.</exception>
        public Trip(Guid passengerId, Route route, Fare fare, VehicleType vehicleType)
            : base(Guid.NewGuid())
        {
            if (passengerId == Guid.Empty) throw new ArgumentException("Id hành khách không hợp lệ.", nameof(passengerId));
            _passengerId = passengerId;
            _tripRoute = route ?? throw new ArgumentNullException(nameof(route));
            _tripFare = fare ?? throw new ArgumentNullException(nameof(fare));
            if (vehicleType == 0) throw new ArgumentException("Loại xe không hợp lệ.", nameof(vehicleType));
            _tripVehicleType = vehicleType;
            _requestAt = DateTime.UtcNow;

            // Set initial state and raise domain events
            _currentState = new RequestedState();
            AddEvent(new TripRequestedEvent(Id, _passengerId, _tripRoute.Pickup, _tripRoute.Destination, _tripVehicleType));
        }

        /// <summary>
        /// Constructor để tái tạo đối tượng chuyến đi từ cơ sở dữ liệu.
        /// </summary>
        [Newtonsoft.Json.JsonConstructor]
        public Trip(
            Guid id,
            Guid passengerId,
            Guid? driverId,
            Route tripRoute,
            Fare tripFare,
            VehicleType tripVehicleType,
            DateTime requestAt,
            string status,
            bool isPaid)
            : base(id)
        {
            _passengerId = passengerId;
            _driverId = driverId;
            _tripRoute = tripRoute;
            _tripFare = tripFare;
            _tripVehicleType = tripVehicleType;
            _requestAt = requestAt;
            Status = status;
            _isPaid = isPaid;
        }

        #endregion

#region State Management

        /// <summary>
        /// (Internal) Chuyển đổi trạng thái của chuyến đi một cách thread-safe.
        /// Sử dụng Interlocked.Exchange để đảm bảo atomic state transition.
        /// Chỉ được gọi bởi các đối tượng <see cref="ITripState"/>.
        /// </summary>
        /// <param name="newState">Trạng thái mới cần chuyển đến.</param>
        internal void TransitionTo(ITripState newState)
        {
            if (newState == null)
                throw new ArgumentNullException(nameof(newState));
            Interlocked.Exchange(ref _currentState, newState);
        }

        /// <summary>
        /// (Internal) Gán ID tài xế cho chuyến đi. Chỉ được gọi bởi <see cref="MatchedState"/>.
        /// </summary>
        /// <param name="driverId">ID của tài xế được ghép cặp.</param>
        internal void SetDriverId(Guid driverId) => _driverId = driverId;

        #endregion

        #region Public Behavior (Delegated to State)

        /// <summary>
        /// Chuyển chuyến đi sang trạng thái đang tìm tài xế.
        /// </summary>
        public void SetSearching() => _currentState.SetSearching(this);

        /// <summary>
        /// Ghép cặp chuyến đi với một tài xế.
        /// </summary>
        /// <param name="driverId">ID của tài xế được ghép cặp.</param>
        public void MatchDriver(Guid driverId) => _currentState.MatchDriver(this, driverId);

        /// <summary>
        /// Đánh dấu tài xế đã đến điểm đón.
        /// </summary>
        public void MarkAsArrived() => _currentState.MarkAsArrived(this);

        /// <summary>
        /// Bắt đầu chuyến đi.
        /// </summary>
        public void StartTrip() => _currentState.StartTrip(this);

        /// <summary>
        /// Hoàn thành chuyến đi.
        /// </summary>
        public void CompleteTrip() => _currentState.CompleteTrip(this);

        /// <summary>
        /// Hủy chuyến đi.
        /// </summary>
        /// <param name="reason">Lý do hủy chuyến.</param>
        public void Cancel(string reason) => _currentState.Cancel(this, reason);

        /// <summary>
        /// Đánh dấu chuyến đi đã hết thời gian chờ tìm tài xế.
        /// </summary>
        public void MarkTimeout() => _currentState.MarkTimeout(this);

        #endregion

        #region Payment

        /// <summary>
        /// Xác nhận rằng chuyến đi đã được thanh toán.
        /// </summary>
        /// <remarks>
        /// Một chuyến đi chỉ có thể được thanh toán sau khi đã được ghép cặp với một tài xế.
        /// </remarks>
        /// <exception cref="InvalidOperationException">Ném khi chuyến đi đã được thanh toán trước đó hoặc chưa có tài xế.</exception>
        public void ConfirmPayment()
        {
            if (_isPaid)
                throw new InvalidOperationException("Chuyến đi này đã được thanh toán.");

            if (!DriverId.HasValue)
                throw new InvalidOperationException("Chuyến đi chưa có tài xế nên không thể thanh toán.");

            _isPaid = true;
            AddEvent(new TripPaidEvent(Id, PassengerId, DriverId.Value, _tripFare.TotalAmount));
        }

        #endregion
    }
}
