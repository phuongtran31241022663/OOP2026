using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Common.Constants
{
    public static class FareConstants
    {
        public const decimal CommissionRate = 0.20m; // 20% phí sàn
        public const decimal MinimumFare = 12000m;   // Giá tối thiểu cho 1 chuyến
        public const decimal WaitingFeePerMinute = 1000m;

        public static class Motorbike
        {
            public const decimal BaseFare = 12000m;  // 2km đầu
            public const decimal PricePerKm = 4000m;
        }

        public static class Car
        {
            public const decimal BaseFare = 25000m;  // 2km đầu
            public const decimal PricePerKm = 12000m;
        }
    }
}
