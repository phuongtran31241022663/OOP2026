using System;

namespace Presentation.Extensions
{
    /// <summary>
    /// Extension methods for Presentation layer functionality.
    /// Provides helpers for API responses, validation, and formatting.
    /// 
    /// Phương thức mở rộng cho chức năng lớp Presentation.
    /// Cung cấp trợ giúp cho phản hồi API, xác thực và định dạng.
    /// </summary>
    public static class PresentationExtensions
    {
        /// <summary>
        /// Converts a Result object to an ApiResponse for API endpoints.
        /// </summary>
        public static Contracts.Responses.ApiResponse ToApiResponse<T>(
            this Common.Primitives.Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Contracts.Responses.ApiResponse.CreateSuccess(result.Value);
            }
            else
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Message));
                return Contracts.Responses.ApiResponse.CreateFailure(errors);
            }
        }

        /// <summary>
        /// Converts a Result object to a generic typed ApiResponse for API endpoints.
        /// </summary>
        public static Contracts.Responses.ApiResponse<T> ToApiResponse<T>(
            this Common.Primitives.Result<T> result,
            Func<T, object>? transform = null)
        {
            if (result.IsSuccess)
            {
                var data = transform != null ? transform(result.Value) : result.Value;
                return Contracts.Responses.ApiResponse<T>.CreateSuccess((T)data!);
            }
            else
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Message));
                return Contracts.Responses.ApiResponse<T>.CreateFailure(errors);
            }
        }

        /// <summary>
        /// Formats a decimal amount as currency string.
        /// </summary>
        public static string FormatAsCurrency(this decimal amount, string currencySymbol = "$")
        {
            return $"{currencySymbol}{amount:N2}";
        }

        /// <summary>
        /// Formats a DateTime for display in API responses.
        /// </summary>
        public static string FormatForApi(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
        }

        /// <summary>
        /// Validates that a collection is not empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        /// <summary>
        /// Paginates a collection.
        /// </summary>
        public static IEnumerable<T> Paginate<T>(
            this IEnumerable<T> source,
            int pageNumber,
            int pageSize)
        {
            if (pageNumber < 1) pageNumber = 1;
            if (pageSize < 1) pageSize = 10;

            return source
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
