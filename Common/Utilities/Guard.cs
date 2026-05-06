using System;

namespace Common.Utilities
{
    /// <summary>
    /// Argument validation helpers.
    /// </summary>
    public static class Guard
    {
        public static void AgainstNull(object value, string paramName)
        {
            if (value == null)
                throw new ArgumentNullException(paramName);
        }

        public static void AgainstNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException($"{paramName} cannot be null or empty.", paramName);
        }

        public static void AgainstNullOrWhiteSpace(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException($"{paramName} cannot be null or whitespace.", paramName);
        }

        public static void AgainstDefault(Guid value, string paramName)
        {
            if (value == Guid.Empty)
                throw new ArgumentException($"{paramName} cannot be empty GUID.", paramName);
        }

        public static void AgainstNegativeOrZero(decimal value, string paramName)
        {
            if (value <= 0)
                throw new ArgumentException($"{paramName} must be greater than zero.", paramName);
        }

        public static void AgainstNegativeOrZero(double value, string paramName)
        {
            if (value <= 0)
                throw new ArgumentException($"{paramName} must be greater than zero.", paramName);
        }

        public static void AgainstNegativeOrZero(int value, string paramName)
        {
            if (value <= 0)
                throw new ArgumentException($"{paramName} must be greater than zero.", paramName);
        }

        public static void AgainstRange(int value, int min, int max, string paramName)
        {
            if (value < min || value > max)
                throw new ArgumentException($"{paramName} must be between {min} and {max}.", paramName);
        }
    }
}