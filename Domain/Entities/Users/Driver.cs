using Domain.Events;
using Domain.States;
using Domain.States.Drivers;
using Domain.ValueObjects;
using Newtonsoft.Json.Linq;
using System;

namespace Domain.Entities.Users
{
    /// <summary>
    /// Đại diện cho một tài xế trong hệ thống.
    /// </summary>
    /// <remarks>
    /// Lớp này kế thừa từ <see cref="User"/> và quản lý các thông tin, trạng thái và hành vi đặc thù của tài xế.
    /// <para>Sử dụng State Pattern (<see cref="IDriverState"/>) để quản lý trạng thái hoạt động: Offline, Available, OnTrip.</para>
    /// <para>Quan hệ: Association với <see cref="Vehicle"/> (qua VehicleId), Composition với <see cref="Money"/> (Wallet, Income).</para>
    /// </remarks>
    public class Driver : User
    {
        #region Fields
        private readonly string _licenseNumber;
        private readonly Guid _vehicleId;

        private IDriverState _currentState;
        private Location _position;
        private Money _wallet;
        private Money _income;
        private int _totalTrips;
        private int _ratingSum;
        private int _totalReviews;

        #endregion

        #region Properties

        /// <summary>
        /// Lấy trạng thái hoạt động hiện tại của tài xế dưới dạng chuỗi (ví dụ: "Available", "OnTrip").
        /// </summary>
        public string Status
        {
            get
            {
                if (_currentState == null)
                    return "Unknown";
                string name = _currentState.GetType().Name;
                if (name.EndsWith("State"))
                    name = name.Substring(0, name.Length - 5);
                return name;
            }
        }

        /// <summary>
        /// Điểm đánh giá trung bình của tài xế, tính từ 1 đến 5.
        /// </summary>
        public decimal AverageRating => TotalReviews == 0 ? 0 : (decimal)RatingSum / TotalReviews;

        /// <summary>
        /// Vị trí hiện tại của tài xế trên bản đồ.
        /// </summary>
        public Location Position
        {
            get => _position;
            private set => _position = value ?? throw new ArgumentNullException(nameof(Position));
        }
        /// <summary>
        /// ID của phương tiện mà tài xế đang sử dụng.
        /// </summary>
        public Guid VehicleId => _vehicleId;

        /// <summary>
        /// Số dư ví điện tử của tài xế.
        /// </summary>
        public Money Wallet { get => _wallet; private set => _wallet = value; }

        /// <summary>
        /// Tổng thu nhập của tài xế (sau khi trừ hoa hồng).
        /// </summary>
        public Money Income { get => _income; private set => _income = value; }

        /// <summary>
        /// Tổng số chuyến đi đã hoàn thành.
        /// </summary>
        public int TotalTrips
        {
            get => _totalTrips;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(TotalTrips), "Tổng số chuyến không được âm.");
                _totalTrips = value;
            }
        }
        /// <summary>
        /// Tổng điểm đánh giá đã nhận.
        /// </summary>
        public int RatingSum
        {
            get => _ratingSum;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(RatingSum), "Tổng điểm không được âm.");
                _ratingSum = value;
            }
        }

        /// <summary>
        /// Tổng số lượt đánh giá đã nhận.
        /// </summary>
        public int TotalReviews
        {
            get => _totalReviews;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(TotalReviews), "Số lượt đánh giá không được âm.");
                _totalReviews = value;
            }
        }

        /// <summary>
        /// Số giấy phép lái xe của tài xế.
        /// </summary>
        public string LicenseNumber => _licenseNumber;

        #endregion
        
        #region Constructors

        /// <summary>
        /// Constructor private cho mục đích deserialization (ví dụ: từ JSON).
        /// </summary>
        private Driver()
        {
        }

        /// <summary>
        /// Khởi tạo một tài xế mới với mật khẩu thô (sẽ được băm).
        /// </summary>
        /// <param name="name">Tên của tài xế.</param>
        /// <param name="phone">Số điện thoại.</param>
        /// <param name="password">Mật khẩu thô.</param>
        /// <param name="licenseNumber">Số giấy phép lái xe.</param>
        /// <param name="vehicleId">ID của phương tiện.</param>
        /// <param name="position">Vị trí ban đầu.</param>
        public Driver(
            string name,
            string phone,
            string password,
            string licenseNumber,
            Guid vehicleId,
            Location position)
            : base(name, phone, password)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new ArgumentException("Số giấy phép lái xe không được để trống.", nameof(licenseNumber));
            if (vehicleId == Guid.Empty)
                throw new ArgumentException("Id phương tiện không hợp lệ.", nameof(vehicleId));
            if (position == null)
                throw new ArgumentNullException(nameof(position));
            _licenseNumber = licenseNumber;
            Position = position;
            _vehicleId = vehicleId;
            _currentState = new DriverOfflineState();
            Wallet = new Money(0);
            Income = new Money(0);
            TotalTrips = 0;
        }

        /// <summary>
        /// Khởi tạo một tài xế từ dữ liệu đã có (ví dụ: từ database) với mật khẩu đã được băm.
        /// </summary>
        /// <param name="id">ID của tài xế.</param>
        /// <param name="name">Tên của tài xế.</param>
        /// <param name="phone">Số điện thoại.</param>
        /// <param name="hashedPassword">Mật khẩu đã được băm.</param>
        /// <param name="licenseNumber">Số giấy phép lái xe.</param>
        /// <param name="vehicleId">ID của phương tiện.</param>
        /// <param name="position">Vị trí ban đầu.</param>
        public Driver(
            Guid id,
            string name,
            string phone,
            string hashedPassword,
            string licenseNumber,
            Guid vehicleId,
            Location position)
            : base(id, name, phone, hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new ArgumentException("Số giấy phép lái xe không được để trống.", nameof(licenseNumber));
            if (vehicleId == Guid.Empty)
                throw new ArgumentException("Id phương tiện không hợp lệ.", nameof(vehicleId));
            if (position == null)
                throw new ArgumentNullException(nameof(position));

            _licenseNumber = licenseNumber;
            Position = position;
            _vehicleId = vehicleId;
            _currentState = new DriverOfflineState();
            Wallet = new Money(0);
            Income = new Money(0);
            TotalTrips = 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Kiểm tra xem tài xế có đang ở trạng thái sẵn sàng nhận chuyến hay không.
        /// </summary>
        /// <returns><c>true</c> nếu trạng thái là <see cref="DriverAvailableState"/>.</returns>
        public bool IsAvailable() => _currentState is DriverAvailableState;

        /// <summary>
        /// Kiểm tra xem tài xế có đang trong một chuyến đi hay không.
        /// </summary>
        /// <returns><c>true</c> nếu trạng thái là <see cref="DriverOnTripState"/>.</returns>
        public bool IsOnTrip() => _currentState is DriverOnTripState;

        /// <summary>
        /// Kiểm tra xem tài xế có đang ở trạng thái nghỉ (không hoạt động) hay không.
        /// </summary>
        /// <returns><c>true</c> nếu trạng thái là <see cref="DriverOfflineState"/>.</returns>
        public bool IsOffline() => _currentState is DriverOfflineState;

        /// <summary>
        /// Chuyển tài xế sang trạng thái sẵn sàng nhận chuyến.
        /// </summary>
        public void SetAvailable() => _currentState.SetAvailable(this);

        /// <summary>
        /// Chuyển tài xế sang trạng thái đang trong chuyến đi.
        /// </summary>
        public void SetOnTrip() => _currentState.SetOnTrip(this);

        /// <summary>
        /// Chuyển tài xế sang trạng thái nghỉ.
        /// </summary>
        public void SetOffline() => _currentState.SetOffline(this);

        /// <summary>
        /// Cập nhật vị trí hiện tại của tài xế.
        /// </summary>
        /// <param name="newPosition">Vị trí mới.</param>
        /// <exception cref="ArgumentNullException">Ném khi <paramref name="newPosition"/> là <c>null</c>.</exception>
        public void UpdatePosition(Location newPosition)
        {
            if (newPosition == null)
                throw new ArgumentNullException(nameof(newPosition));

            _position = newPosition;
            AddEvent(new DriverLocationUpdatedEvent(Id, newPosition));
        }

        /// <summary>
        /// Ghi nhận một chuyến đi đã hoàn thành cho tài xế.
        /// </summary>
        public void AddTrip()
        {
            _totalTrips++;
        }

        /// <summary>
        /// Cập nhật thông tin đánh giá cho tài xế.
        /// </summary>
        /// <param name="rating">Điểm đánh giá từ 1 đến 5.</param>
        /// <exception cref="ArgumentOutOfRangeException">Ném khi <paramref name="rating"/> nằm ngoài khoảng [1, 5].</exception>
        public void UpdateReviews(int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException(nameof(rating), "Điểm đánh giá phải từ 1 đến 5.");
            TotalReviews++;
            RatingSum += rating;
        }

        /// <summary>
        /// Nạp tiền vào ví của tài xế.
        /// </summary>
        /// <param name="amount">Số tiền cần nạp.</param>
        /// <exception cref="ArgumentException">Ném khi số tiền nạp nhỏ hơn hoặc bằng 0.</exception>
        public void DepositToWallet(Money amount)
        {
            if (amount.Amount <= 0)
                throw new ArgumentException("Số tiền nạp phải lớn hơn 0.", nameof(amount));

            Wallet += amount;
        }

        /// <summary>
        /// Thanh toán phí hoa hồng cho một chuyến đi và ghi nhận thu nhập.
        /// </summary>
        /// <param name="fare">Thông tin giá cước của chuyến đi đã hoàn thành.</param>
        /// <exception cref="InvalidOperationException">Ném khi số dư ví không đủ để trả hoa hồng.</exception>
        public void PayCommission(Fare fare)
        {
            if (Wallet.Amount < fare.Commission.Amount)
                throw new InvalidOperationException("Số dư ví không đủ để trả hoa hồng.");

            Wallet -= fare.Commission;
            Income += fare.DriverIncome;
        }
        #endregion

        #region Private Methods
        
        /// <summary>
        /// (Internal) Chuyển đổi trạng thái của tài xế. Chỉ được gọi bởi các đối tượng <see cref="IDriverState"/>.
        /// </summary>
        /// <param name="newState">Trạng thái mới cần chuyển đến.</param>
        internal void TransitionTo(IDriverState newState)
        {
            var oldStateName = Status;
            _currentState = newState;
            AddEvent(new DriverStatusChangedEvent(Id, oldStateName, Status));
        }

        #endregion
    }
}
