using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public sealed class Fare
    {
        public Money Amount { get; }

        public Fare(Money amount)
        {
            Amount = amount;
        }
    }
}
