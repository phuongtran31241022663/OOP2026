using Domain.Enums;
using Domain.ValueObjects;
using System;
using Domain.SharedKernel;

namespace Domain.FareRules
{
    /// <summary>
    /// Quy tắc tính giá cho từng loại phương tiện.
    /// Dependency: FareRule phụ thuộc vào VehicleType (Enum) để xác định loại phương tiện và Fare (Value Object) để tính toán cước.
    /// <summary>
    public class FareRule : Entity
    {
        #region Backing Fields
        private Money _baseFare;
        private Money _pricePerKm;
        private decimal _commissionRate;
        private DateTime _updatedAt;
        #endregion
        public VehicleType VehicleType { get; }

        public Money BaseFare => _baseFare;
        public Money PricePerKm => _pricePerKm;
        public decimal CommissionRate => _commissionRate;
        public DateTime UpdatedAt => _updatedAt;

        // public int Version { get; private set; } = 1;
        protected FareRule() : base(Guid.Empty) { }

        public FareRule(
            VehicleType vehicleType,
            Money baseFare,
            Money pricePerKm,
            decimal commissionRate
        ) : base(Guid.NewGuid())
        {
            VehicleType = vehicleType;
            SetRule(baseFare, pricePerKm, commissionRate);
        }
        private void SetRule(Money baseFare, Money pricePerKm, decimal commissionRate)
        {
            if (baseFare == null)
                throw new ArgumentNullException(nameof(baseFare));

            if (pricePerKm == null)
                throw new ArgumentNullException(nameof(pricePerKm));

            if (baseFare.Amount < 0)
                throw new ArgumentOutOfRangeException(nameof(baseFare), "BaseFare phải >= 0");

            if (pricePerKm.Amount < 0)
                throw new ArgumentOutOfRangeException(nameof(pricePerKm), "PricePerKm phải >= 0");

            if (commissionRate < 0 || commissionRate > 1)
                throw new ArgumentOutOfRangeException(nameof(commissionRate), "CommissionRate phải trong [0,1]");

            _baseFare = baseFare;
            _pricePerKm = pricePerKm;
            _commissionRate = commissionRate;
            _updatedAt = DateTime.UtcNow;
        }
        public void UpdateRule(Money baseFare, Money pricePerKm, decimal commissionRate)
        {
            SetRule(baseFare, pricePerKm, commissionRate);
        }
        // Tính cước dựa trên khoảng cách (km)
        public Fare CalculateFare(double distanceKm)
        {
            if (distanceKm < 0)
                throw new ArgumentOutOfRangeException(nameof(distanceKm), "Distance phải >= 0");

            decimal totalAmount =
                BaseFare.Amount +
                ((decimal)distanceKm * PricePerKm.Amount);

            decimal commissionAmount = totalAmount * _commissionRate;

            Money total = new Money(totalAmount);
            Money commission = new Money(commissionAmount);

            return new Fare(total, commission);
        }

    }
}
