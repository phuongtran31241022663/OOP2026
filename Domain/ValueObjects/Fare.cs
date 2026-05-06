using Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public sealed class Fare : ValueObject
    {
        #region Fields
        private readonly Money _totalAmount;
        private readonly Money _commission;
        private readonly Money _driverIncome;
        #endregion
        #region Properties   
        public Money TotalAmount => _totalAmount;
        public Money Commission => _commission;
        public Money DriverIncome => _driverIncome;
        #endregion
        #region Constructors

        public Fare(Money totalAmount, Money commission)
        {
            if (totalAmount == null) throw new ArgumentNullException(nameof(totalAmount));
            if (commission == null) throw new ArgumentNullException(nameof(commission));
            if (totalAmount.Amount < 0) throw new ArgumentException("Tổng tiền không thể âm.", nameof(totalAmount));
            if (commission.Amount < 0) throw new ArgumentException("Hoa hồng không thể âm.", nameof(commission));
            if (!string.Equals(totalAmount.Currency, commission.Currency, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Tổng tiền và Hoa hồng phải cùng loại tiền tệ.");
            if (commission.Amount > totalAmount.Amount)
                throw new ArgumentException("Hoa hồng không thể lớn hơn tổng cước.");

            _totalAmount = totalAmount;
            _commission = commission;
            _driverIncome = new Money(
                totalAmount.Amount - commission.Amount,
                totalAmount.Currency
            );
        }
        #endregion
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TotalAmount;
            yield return Commission;
        }
    }
}