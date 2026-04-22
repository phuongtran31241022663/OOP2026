using Domain.Enums;
using Domain.StateMachines;
using Domain.ValueObjects;
using System;
using static Domain.StateMachines.DriverStateMachine;
using Domain.Users.Drivers.Events;
using Domain.Vehicles;

namespace Domain.Users.Drivers
{
    public class Driver : User
    {
        #region Fields

        #endregion
        #region Properties
        public DriverStatus Status { get; private set; } = DriverStatus.Offline;
        public string LicenseNumber { get; private set; }
        public Location Position { get; private set; }
        public Guid VehicleId { get; private set; }
        public Money Wallet { get; private set; }
        public Money Income { get; private set; }
        public int TotalTrips { get; private set; }
        public int Version { get; set; } = 1;
        public int RatingSum { get; private set; }
        public int TotalReviews { get; private set; }
        public decimal AverageRating => TotalReviews == 0 ? 0 : (decimal)RatingSum / TotalReviews;
        #endregion
        #region Constructors

        public Driver(
            string name,
            string phone,
            string password,
            Guid vehicleId,
            Location position)
            : base(Guid.NewGuid(), name, phone, password)
        {
            Position = position;
            VehicleId = vehicleId;
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
            Guid vehicleId,
            Location position)
            : base(id, name, phone, password)
        {
            Position = position;
            VehicleId = vehicleId;
            Status = DriverStatus.Offline;
            Wallet = new Money(0);
            Income = new Money(0);
            TotalTrips = 0;
        }

        #endregion
        #region Tr?ng th�i l�i xe
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
            if (!CanTransition(Status, DriverStatus.OnTrip))
                throw new InvalidOperationException(
                    $"Không thể chuyển từ '{Status}' sang 'OnTrip'.");

            DriverStatus oldStatus = Status;
            Status = DriverStatus.OnTrip;
            AddEvent(new DriverStatusChangedEvent(Id, oldStatus, Status));
        }

        public void SetOffline()
        {
            if (!IsActive)
                throw new InvalidOperationException("Tài xế đã bị vô hiệu hóa.");

            if (Status == DriverStatus.Offline)
                return;

            if (Status == DriverStatus.OnTrip)
                throw new InvalidOperationException(
                    "Quy tắc nghiệp vụ: Không thể ngắt kết nối khi đang chạy chuyến.");

            if (!CanTransition(Status, DriverStatus.Offline))
                throw new InvalidOperationException(
                    $"Không thể chuyển từ trạng thái '{Status}' sang 'Offline'.");

            DriverStatus oldStatus = Status;
            Status = DriverStatus.Offline;

            AddEvent(new DriverStatusChangedEvent(Id, oldStatus, Status));
        }

        #endregion


        // -- Chuy?n di -------------------------------------------------------
        public void AddTrip()
        {
            TotalTrips++;
        }
        public void UpdateReviews(int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentException("Invalid score");

                TotalReviews++;
            
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
