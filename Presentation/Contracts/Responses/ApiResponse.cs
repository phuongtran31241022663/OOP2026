using System;

namespace Presentation.Contracts.Responses
{
    /// <summary>
    /// Base API response wrapper for all API endpoints.
    /// Provides consistent response structure across the application.
    /// 
    /// Trình bao bọc phản hồi API cơ sở cho tất cả các endpoint.
    /// Cung cấp cấu trúc phản hồi nhất quán trên toàn ứng dụng.
    /// </summary>
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public static ApiResponse CreateSuccess(object? data = null, string message = "Operation completed successfully")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse CreateFailure(string message, object? data = null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                Data = data
            };
        }
    }

    /// <summary>
    /// Generic API response wrapper with typed data.
    /// </summary>
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        public static ApiResponse<T> CreateSuccess(T data, string message = "Operation completed successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data
            };
        }

        public static ApiResponse<T> CreateFailure(string message)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
    }

    /// <summary>
    /// Paged response wrapper for paginated API responses.
    /// </summary>
    public class PagedResponse<T> : ApiResponse<IEnumerable<T>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);
        public bool HasPreviousPage => PageNumber > 1;
        public bool HasNextPage => PageNumber < TotalPages;

        public static PagedResponse<T> CreateSuccess(
            IEnumerable<T> data,
            int pageNumber,
            int pageSize,
            int totalItems,
            string message = "Page retrieved successfully")
        {
            return new PagedResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        public static PagedResponse<T> CreateFailure(string message)
        {
            return new PagedResponse<T>
            {
                Success = false,
                Message = message,
                Data = null,
                PageNumber = 0,
                PageSize = 0,
                TotalItems = 0
            };
        }
    }

    /// <summary>
    /// Error response for API error scenarios.
    /// </summary>
    public class ErrorResponse
    {
        public string Code { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string? Details { get; set; }
        public Dictionary<string, string[]>? Errors { get; set; }

        public static ErrorResponse CreateValidationError(Dictionary<string, string[]> errors)
        {
            return new ErrorResponse
            {
                Code = "VALIDATION_FAILED",
                Message = "One or more validation errors occurred.",
                Errors = errors
            };
        }

        public static ErrorResponse CreateError(string code, string message, string? details = null)
        {
            return new ErrorResponse
            {
                Code = code,
                Message = message,
                Details = details
            };
        }
    }
}
