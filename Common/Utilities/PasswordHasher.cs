using System;
using System.Text;

namespace Common.Utilities
{
    // Mã hóa mật khẩu
    public static class PasswordHasher
    {
        /// <summary>
        /// Encode the raw password as Base64.
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty", nameof(password));

            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Verify a raw password against a stored Base64‑encoded hash.
        /// </summary>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                return false;

            try
            {
                var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(hashedPassword));
                // constant‑time comparison to avoid timing attacks
                return FixedTimeEquals(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(decoded));
            }
            catch
            {
                // malformed hash
                return false;
            }
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}