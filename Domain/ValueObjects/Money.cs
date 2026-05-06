using System;
using System.Collections.Generic;
using Domain.SharedKernel;
// Thấy cái này không quá cần thiết nhưng mà cũng hay cũng kệ
namespace Domain.ValueObjects
{
    /// <summary>
    /// Value Object representing monetary amount with currency.
    /// Bất biến (immutable) và so sánh theo giá trị (value-based equality).
    /// </summary>
    public sealed class Money : ValueObject
    {
        #region Fields
        private readonly decimal _amount;
        private readonly string _currency;
        #endregion
        #region Properties
       public decimal Amount => _amount;
        public string Currency => _currency;
        #endregion

        [Newtonsoft.Json.JsonConstructor]
        public Money(decimal amount, string currency)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount), "Số tiền không thể âm.");
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Loại tiền tệ không được để trống.", nameof(currency));

            _amount = amount;
            _currency = currency;
        }


        public Money(decimal amount) : this(amount, "VND") { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        private static void EnsureSameCurrency(Money a, Money b)
        {
            if (!string.Equals(a.Currency, b.Currency, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Đơn vị tiền tệ không khớp.");
        }

        public static Money operator +(Money a, Money b)
        {
            ValidateOperands(a, b);
            EnsureSameCurrency(a, b);
            return new Money(a.Amount + b.Amount, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            ValidateOperands(a, b);
            EnsureSameCurrency(a, b);
            decimal result = a.Amount - b.Amount;
            if (result < 0)
                throw new InvalidOperationException("Số tiền không thể âm.");

            return new Money(result, a.Currency);
        }

        public static bool operator <(Money a, Money b)
        {
            ValidateOperands(a, b);
            EnsureSameCurrency(a, b);
            return a.Amount < b.Amount;
        }

        public static bool operator >(Money a, Money b)
        {
            ValidateOperands(a, b);
            EnsureSameCurrency(a, b);
            return a.Amount > b.Amount;
        }

        public static bool operator <=(Money a, Money b)
        {
            ValidateOperands(a, b);
            EnsureSameCurrency(a, b);
            return a.Amount <= b.Amount;
        }

        public static bool operator >=(Money a, Money b)
        {
            ValidateOperands(a, b);
            EnsureSameCurrency(a, b);
            return a.Amount >= b.Amount;
        }
        private static void ValidateOperands(Money a, Money b)
        {
            if (a == null) throw new ArgumentNullException(nameof(a), "Toán hạng bên trái không được null.");
            if (b == null) throw new ArgumentNullException(nameof(b), "Toán hạng bên phải không được null.");
        }
        public override string ToString()
        {
            return string.Format("{0:N0} {1}", Amount, Currency);
        }
    }
}
