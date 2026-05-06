using System;
using System.Text.RegularExpressions;

namespace Common.Utilities
{
    /// <summary>
    /// Vietnamese phone number validation.
    /// </summary>
    public static class PhoneValidator
    {
        /// <summary>
        /// Validates Vietnamese phone number (10 digits, starts with 0).
        /// </summary>
        public static bool IsValid(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            string trimmed = phone.Trim();
            return Regex.IsMatch(trimmed, @"^0[0-9]{9}$");
        }

        /// <summary>
        /// Validates Vietnamese phone number (9-11 digits, starts with 0).
        /// </summary>
        public static bool IsValidFlexible(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            string trimmed = phone.Trim();
            return Regex.IsMatch(trimmed, @"^0[0-9]{9,11}$");
        }
    }
}