using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Common.Extensions
{
    public static class DecimalExtensions
    {
        #region Financial Logic
        /// <summary>
        /// Làm tròn tiền theo đơn vị nghìn đồng (VND).
        /// Ví dụ: 12300 -> 12000, 12600 -> 13000.
        /// </summary>
        public static decimal RoundToVnd(this decimal amount)
        {
            return Math.Round(amount / 1000m, MidpointRounding.AwayFromZero) * 1000m;
        }

        /// <summary>
        /// Tính toán số tiền sau khi trừ đi tỷ lệ phần trăm (phí sàn).
        /// </summary>
        public static decimal SubtractPercentage(this decimal amount, decimal percentage)
        {
            if (percentage < 0 || percentage > 1) return amount;
            return amount * (1 - percentage);
        }
        #endregion

        #region Formatting
        /// <summary>
        /// Định dạng tiền tệ theo chuẩn Việt Nam (ví dụ: 50.000đ).
        /// </summary>
        public static string ToVndCurrency(this decimal amount)
        {
            // Định dạng: 100,000.00 -> 100.000
            // Sử dụng "N0" để không lấy số thập phân
            return string.Format("{0:N0}đ", amount).Replace(",", ".");
        }

        /// <summary>
        /// Định dạng khoảng cách (ví dụ: 1.2 km).
        /// </summary>
        public static string ToDistanceString(this decimal distance)
        {
            return string.Format("{0:0.0} km", distance);
        }
        #endregion
    }
}
