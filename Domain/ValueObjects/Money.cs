using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class Money
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Money(decimal amount, string currency = "VND")
        {
            if (amount < 0)
                throw new ArgumentException("Amount cannot be negative");

            Amount = decimal.Round(amount, 2);
            Currency = currency;
        }

        public Money Add(Money other)
        {
            EnsureSameCurrency(other);
            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            EnsureSameCurrency(other);

            if (Amount < other.Amount)
                throw new InvalidOperationException("Insufficient funds");

            return new Money(Amount - other.Amount, Currency);
        }

        private void EnsureSameCurrency(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Currency mismatch");
        }
    }
}
