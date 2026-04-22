using System;
using System.Text.RegularExpressions;


namespace Common.Extensions
{
    public static class StringExtensions
    {
        #region Validation Logic
        /// <summary>
        /// Kiểm tra xem chuỗi có phải là số điện thoại Việt Nam hợp lệ (10 số) không.
        /// </summary>
        public static bool IsValidPhone(this string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            // Regex cho SĐT Việt Nam: Bắt đầu bằng 0, theo sau là 9 chữ số
            return Regex.IsMatch(phone, @"^0[0-9]{9}$");
        }

        /// <summary>
        /// Kiểm tra định dạng Email cơ bản.
        /// </summary>
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
        #endregion

        #region Sanitization & Transformation
        /// <summary>
        /// Viết hoa chữ cái đầu của mỗi từ (Dùng cho Tên người, Tên đường).
        /// Ví dụ: "nguyễn văn a" -> "Nguyễn Văn A"
        /// </summary>
        public static string ToTitleCase(this string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return input;

            var words = input.ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
            }
            return string.Join(" ", words);
        }

        /// <summary>
        /// Rút gọn chuỗi nếu quá dài (Dùng cho hiển thị ghi chú trên Map/Grid).
        /// </summary>
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }
        #endregion
    }
}
