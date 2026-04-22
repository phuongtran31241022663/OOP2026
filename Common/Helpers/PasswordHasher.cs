using System;
using System.Security.Cryptography;
using System.Text;

namespace Common.Helpers
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100000;
        private const int LegacyIterations = 10000;
        private const string CurrentFormat = "pbkdf2-sha256";

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }

            return result == 0;
        }

        /// <summary>
        /// Hash password with PBKDF2 and store as algorithm$iterations$salt$hash.
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Password cannot be null or empty");
            }

            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                return $"{CurrentFormat}${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
            }
        }

        /// <summary>
        /// Verify password against known hash formats.
        /// Supports current format and legacy formats for migration.
        /// </summary>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
            {
                return false;
            }

            bool isMatch;
            if (TryVerifyCurrentFormat(password, hashedPassword, out isMatch))
            {
                return isMatch;
            }

            if (TryVerifyLegacyPbkdf2Format(password, hashedPassword, out isMatch))
            {
                return isMatch;
            }

            if (TryVerifyLegacyPlainText(password, hashedPassword, out isMatch))
            {
                return isMatch;
            }

            System.Diagnostics.Trace.TraceWarning("Password hash verification failed due to unsupported format");
            return false;
        }

        /// <summary>
        /// Indicates whether a stored password should be rehashed with current policy.
        /// </summary>
        public static bool NeedsRehash(string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
            {
                return true;
            }

            var parts = hashedPassword.Split('$');
            if (parts.Length != 4 || !string.Equals(parts[0], CurrentFormat, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (!int.TryParse(parts[1], out int existingIterations))
            {
                return true;
            }

            return existingIterations < Iterations;
        }

        private static bool TryVerifyCurrentFormat(string password, string hashedPassword, out bool isMatch)
        {
            isMatch = false;

            var parts = hashedPassword.Split('$');
            if (parts.Length != 4 || !string.Equals(parts[0], CurrentFormat, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!int.TryParse(parts[1], out int iterations) || iterations <= 0)
            {
                System.Diagnostics.Trace.TraceWarning("Password hash verification failed due to invalid iteration count; format not recognized");
                return false;  // SECURITY FIX: return false to try other formats, not true
            }

            try
            {
                byte[] salt = Convert.FromBase64String(parts[2]);
                byte[] expectedHash = Convert.FromBase64String(parts[3]);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(expectedHash.Length);
                    isMatch = FixedTimeEquals(hash, expectedHash);
                    return true;
                }
            }
            catch (FormatException)
            {
                System.Diagnostics.Trace.TraceWarning("Password hash verification failed due to invalid base64 payload; format not recognized");
                return false;  // SECURITY FIX: return false to try other formats
            }
            catch (CryptographicException)
            {
                System.Diagnostics.Trace.TraceWarning("Password hash verification failed due to cryptographic error; format not recognized");
                return false;  // SECURITY FIX: return false to try other formats
            }
        }

        private static bool TryVerifyLegacyPbkdf2Format(string password, string hashedPassword, out bool isMatch)
        {
            isMatch = false;

            var parts = hashedPassword.Split(':');
            int iterations;
            string saltPart;
            string hashPart;

            if (parts.Length == 3 && int.TryParse(parts[0], out iterations) && iterations > 0)
            {
                saltPart = parts[1];
                hashPart = parts[2];
            }
            else if (parts.Length == 2)
            {
                iterations = LegacyIterations;
                saltPart = parts[0];
                hashPart = parts[1];
            }
            else
            {
                return false;
            }

            try
            {
                byte[] salt = Convert.FromBase64String(saltPart);
                byte[] expectedHash = Convert.FromBase64String(hashPart);

                // Legacy PBKDF2 format defaults to SHA1 on .NET Framework.
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                {
                    byte[] hash = pbkdf2.GetBytes(expectedHash.Length);
                    isMatch = FixedTimeEquals(hash, expectedHash);
                    return true;
                }
            }
            catch (FormatException)
            {
                System.Diagnostics.Trace.TraceWarning("Legacy password hash verification failed due to invalid base64 payload; format not recognized");
                return false;  // SECURITY FIX: return false to try other formats
            }
            catch (CryptographicException)
            {
                System.Diagnostics.Trace.TraceWarning("Legacy password hash verification failed due to cryptographic error; format not recognized");
                return false;  // SECURITY FIX: return false to try other formats
            }
        }

        private static bool TryVerifyLegacyPlainText(string password, string hashedPassword, out bool isMatch)
        {
            isMatch = false;

            // SECURITY FIX: Legacy plain text passwords are deprecated and unsafe
            // Only accept if explicitly marked with "plain$" prefix for migration scenarios
            // Do NOT accept bare passwords that might be plain text
            
            // Reject if it contains any algorithm markers
            if (hashedPassword.Contains("$") && !hashedPassword.StartsWith("plain$", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            if (hashedPassword.Contains(":"))
            {
                return false;
            }

            // Only accept if explicitly marked with "plain$" prefix
            if (hashedPassword.StartsWith("plain$", StringComparison.OrdinalIgnoreCase))
            {
                string stored = hashedPassword.Substring("plain$".Length);
                System.Diagnostics.Trace.TraceWarning(
                    "SECURITY WARNING: Accepting legacy plain text password. User MUST rehash immediately on next login.");
                
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] storedBytes = Encoding.UTF8.GetBytes(stored);
                isMatch = FixedTimeEquals(inputBytes, storedBytes);
                return true;
            }

            // Do NOT accept bare passwords without explicit marker - could be plain text
            return false;
        }
    }
}
