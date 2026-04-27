using System;
using Domain.Enums;
using Domain.ValueObjects;
using Domain.Events;
using Domain.States;
using Domain.States.Drivers;
using Newtonsoft.Json;

namespace Domain.Entities.Users
{
    public class Driver : User
    {
        #region Fields
        private IDriverState _currentState;
        private readonly string _licenseNumber;
        private Location _position;
        private readonly Guid _vehicleId;
        private Money _wallet;
        private Money _income;
        private int _totalTrips;
        private int _ratingSum;
        private int _totalReviews;
        #endregion

        #region JSON Backward Compatibility
        [JsonProperty("Status")]
        private DriverStatus SerializedStatus
        {
            get
            {
                if (_currentState is DriverAvailableState) return DriverStatus.Available;
                if (_currentState is DriverOnTripState) return DriverStatus.OnTrip;
                return DriverStatus.Offline;
            }
            set
            {
                switch (value)
                {
                    case DriverStatus.Available: _currentState = new DriverAvailableState(); break;
                    case DriverStatus.OnTrip: _currentState = new DriverOnTripState(); break;
                    default: _currentState = new DriverOfflineState(); break;
                }
            }
        }
        #endregion

        #region Properties
        [JsonIgnore]
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

        public bool IsAvailable() => _currentState is DriverAvailableState;
        public bool IsOnTrip() => _currentState is DriverOnTripState;
        public bool IsOffline() => _currentState is DriverOfflineState;

        public decimal AverageRating => TotalReviews == 0 ? 0 : (decimal)RatingSum / TotalReviews;
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
            _currentState = new DriverOfflineState();
            Wallet = new Money(0);
            Income = new Money(0);
            TotalTrips = 0;
        }

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
            _currentState = new DriverOfflineState();
            Wallet = new Money(0);
            Income = new Money(0);
            TotalTrips = 0;
        }
        #endregion

        #region State Pattern helpers (internal)
        internal void TransitionTo(IDriverState newState) => _currentState = newState;
        #endregion

        #region Public behavior (delegated to state)
        public void SetAvailable() => _currentState.SetAvailable(this);
        public void SetOnTrip() => _currentState.SetOnTrip(this);
        public void SetOffline() => _currentState.SetOffline(this);
        #endregion

        // -- Chuyến đi -------------------------------------------------------
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
        // -- Tài chính -------------------------------------------------------
        public void DepositToWallet(Money amount)
        {
            if (amount.Amount <= 0)
                throw new ArgumentException("Số tiền nạp phải lớn hơn 0.", nameof(amount));

            Wallet += amount;
        }

        public void PayCommission(Fare fare)
        {
            if (Wallet.Amount < fare.Commission.Amount)
                throw new InvalidOperationException("Số dư ví không đủ để trả hoa hồng.");

            Wallet -= fare.Commission;
            Income += fare.DriverIncome;
        }
        public static string GetDisplayString(string status)
        {
            switch (status)
            {
                case "Available": return "Hoạt động";
                case "OnTrip": return "Đang trong chuyến";
                case "Offline": return "Nghỉ";
                default: return "Không xác định";
            }
        }
    }
}

