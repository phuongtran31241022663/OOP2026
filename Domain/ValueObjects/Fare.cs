using Domain.SharedKernel;
using System;
using System.Collections.Generic;

namespace Domain.ValueObjects
{
    public sealed class Fare : ValueObject
    {
        public Money TotalAmount { get; }
        public Money Commission { get; }
        public Money DriverIncome { get; }

        public Fare(Money totalAmount, Money commission)
        {
            if (commission.Amount > totalAmount.Amount)
                throw new ArgumentException("Hoa hồng không thể lớn hơn tổng cước.");

            TotalAmount = totalAmount;
            Commission = commission;
            DriverIncome = new Money(
                totalAmount.Amount - commission.Amount,
                totalAmount.Currency
            );
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TotalAmount;
            yield return Commission;
        }
    }
}