using System;
using System.Collections.Generic;
using Domain.SharedKernel;

namespace Domain.ValueObjects
{
    /// <summary>
    /// Value Object representing monetary amount with currency.
    /// Bất biến (immutable) và so sánh theo giá trị (value-based equality).
    /// </summary>
    public sealed class Money : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }

        // Constructor for ORM/persistence
        private Money() { }

        public Money(decimal amount, string currency = "VND")
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative");

            Amount = decimal.Round(amount, 2, MidpointRounding.AwayFromZero);
            Currency = string.IsNullOrWhiteSpace(currency)
                ? "VND"
                : currency.Trim().ToUpperInvariant();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }

        private static void EnsureSameCurrency(Money a, Money b)
        {
            if (!string.Equals(a.Currency, b.Currency, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("Currency mismatch");
        }

        public static Money operator +(Money a, Money b)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a), "Left operand cannot be null");
            if (b == null)
                throw new ArgumentNullException(nameof(b), "Right operand cannot be null");
            EnsureSameCurrency(a, b);
            return new Money(a.Amount + b.Amount, a.Currency);
        }

        public static Money operator -(Money a, Money b)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a), "Left operand cannot be null");
            if (b == null)
                throw new ArgumentNullException(nameof(b), "Right operand cannot be null");
            EnsureSameCurrency(a, b);
            decimal result = a.Amount - b.Amount;

            if (result < 0)
                throw new InvalidOperationException("Money cannot be negative");

            return new Money(result, a.Currency);
        }

        public static bool operator <(Money a, Money b)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a), "Left operand cannot be null");
            if (b == null)
                throw new ArgumentNullException(nameof(b), "Right operand cannot be null");
            EnsureSameCurrency(a, b);
            return a.Amount < b.Amount;
        }

        public static bool operator >(Money a, Money b)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a), "Left operand cannot be null");
            if (b == null)
                throw new ArgumentNullException(nameof(b), "Right operand cannot be null");
            EnsureSameCurrency(a, b);
            return a.Amount > b.Amount;
        }

        public static bool operator <=(Money a, Money b)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a), "Left operand cannot be null");
            if (b == null)
                throw new ArgumentNullException(nameof(b), "Right operand cannot be null");
            EnsureSameCurrency(a, b);
            return a.Amount <= b.Amount;
        }

        public static bool operator >=(Money a, Money b)
        {
            if (a == null)
                throw new ArgumentNullException(nameof(a), "Left operand cannot be null");
            if (b == null)
                throw new ArgumentNullException(nameof(b), "Right operand cannot be null");
            EnsureSameCurrency(a, b);
            return a.Amount >= b.Amount;
        }
        public override string ToString()
        {
            return string.Format("{0:N0} {1}", Amount, Currency);
        }
    }
}
