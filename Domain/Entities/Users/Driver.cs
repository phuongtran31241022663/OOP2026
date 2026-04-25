using System;
using Domain.Enums;
using Domain.ValueObjects;
using Domain.Events;
using Domain.StateMachines;

namespace Domain.Entities.Users
{
    public class Driver : User
    {
        #region Fields
        private DriverStatus _status;
        private readonly string _licenseNumber;
        private Location _position;
        private readonly Guid _vehicleId;
        private Money _wallet;
        private Money _income;
        private int _totalTrips;
        private int _ratingSum;
        private int _totalReviews;
        #endregion
        #region Properties
        public decimal AverageRating => TotalReviews == 0 ? 0 : (decimal)RatingSum / TotalReviews;
        public DriverStatus Status { get => _status; private set => _status = value; }
      
        public Location Position { get => _position; set => _position = value; }
        public Guid VehicleId => _vehicleId;
        public Money Wallet { get => _wallet; private set => _wallet = value; }
        public Money Income { get => _income; private set => _income = value; }
        public int TotalTrips { get => _totalTrips; private set => _totalTrips = value; }
        public int RatingSum { get => _ratingSum; private set => _ratingSum = value; }
        public int TotalReviews { get => _totalReviews; private set => _totalReviews = value; }
        public string LicenseNumber => _licenseNumber;
        #endregion
        #region Constructors

        public Driver(
            string name,
            string phone,
            string password,
            string licenseNumber,
            Guid vehicleId,
            Location position)
            : base(Guid.NewGuid(), name, phone, password)
        {
            _licenseNumber = licenseNumber;
            Position = position;
            _vehicleId = vehicleId;
            Status = DriverStatus.Offline;
            Wallet = new Money(0);
            Income = new Money(0);
            TotalTrips = 0;
        }

        // Constructor cho persistence
        public Driver(
            Guid id,
            string name,
            string phone,
            string password,
            string licenseNumber,
            Guid vehicleId,
            Location position)
            : base(id, name, phone, password)
        {
            _licenseNumber = licenseNumber;
            Position = position;
            _vehicleId = vehicleId;
            Status = DriverStatus.Offline;
            Wallet = new Money(0);
            Income = new Money(0);
            TotalTrips = 0;
        }

        #endregion
        #region Trạng thái làm việc
        public void SetAvailable()
        {
            if (Status == DriverStatus.Available)
                return;

            if (!DriverStateMachine.CanTransition(Status, DriverStatus.Available))
            {
                throw new InvalidOperationException(
                    $"Quy tắc nghiệp vụ: Không thể chuyển từ '{Status}' sang 'Available'.");
            }

            DriverStatus oldStatus = Status;
            Status = DriverStatus.Available;

            AddEvent(new DriverStatusChangedEvent(Id, oldStatus, Status));
        }

        public void SetOnTrip()
        {
            if (!DriverStateMachine.CanTransition(Status, DriverStatus.OnTrip))
                throw new InvalidOperationException(
                    $"Không thể chuyển từ '{Status}' sang 'OnTrip'.");

            DriverStatus oldStatus = Status;
            Status = DriverStatus.OnTrip;
            AddEvent(new DriverStatusChangedEvent(Id, oldStatus, Status));
        }

        public void SetOffline()
        {
            if (Status == DriverStatus.Offline)
                return;

            if (Status == DriverStatus.OnTrip)
                throw new InvalidOperationException(
                    "Quy tắc nghiệp vụ: Không thể ngắt kết nối khi đang chạy chuyến.");

            if (!DriverStateMachine.CanTransition(Status, DriverStatus.Offline))
                throw new InvalidOperationException(
                    $"Không thể chuyển từ trạng thái '{Status}' sang 'Offline'.");

            DriverStatus oldStatus = Status;
            Status = DriverStatus.Offline;

            AddEvent(new DriverStatusChangedEvent(Id, oldStatus, Status));
        }

        #endregion


        // -- Chuy?n di -------------------------------------------------------
        public void UpdatePosition(Location newPosition)
        {
            if (newPosition == null)
                throw new ArgumentNullException(nameof(newPosition));

            _position = newPosition;
        }
        public void AddTrip()
        {
            _totalTrips++;
        }
        public void UpdateReviews(int rating)
        {
            if (rating < 1 || rating > 5) throw new ArgumentOutOfRangeException(nameof(rating));
            TotalReviews++;
            RatingSum += rating;
        }
        // -- T�i ch�nh -------------------------------------------------------
        public void DepositToWallet(Money amount)
        {
            if (amount.Amount <= 0)
                throw new ArgumentException("S? ti?n n?p ph?i l?n hon 0.", nameof(amount));

            Wallet += amount;
        }

        public void PayCommission(Fare fare)
        {
            if (Wallet.Amount < fare.Commission.Amount)
                throw new InvalidOperationException("Số dư ví không đủ để trả hoa hồng.");

            Wallet -= fare.Commission;
            Income += fare.DriverIncome;
        }
        public static string GetDisplayString(DriverStatus status)
        {
            switch (status)
            {
                case DriverStatus.Available: return "Hoạt động";
                case DriverStatus.OnTrip: return "Đang trong chuyến";
                case DriverStatus.Offline: return "Nghỉ";
                default: return "Không xác định";
            }
        }

    }
}
