using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Presentation.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions globally and converting them to consistent error responses.
    /// Logs exceptions and returns appropriate HTTP status codes.
    /// 
    /// Middleware để xử lý các ngoại lệ trên toàn cầu và chuyển đổi chúng thành phản hồi lỗi nhất quán.
    /// Ghi nhật ký các ngoại lệ và trả về các mã trạng thái HTTP thích hợp.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, exception);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new Presentation.Contracts.Responses.ErrorResponse();

            // Map exception type to HTTP status code and error code
            switch (exception)
            {
                case Application.Exceptions.ResourceNotFoundException notFound:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response = Presentation.Contracts.Responses.ErrorResponse.CreateError(
                        "RESOURCE_NOT_FOUND",
                        notFound.Message);
                    break;

                case Application.Exceptions.UseCaseValidationException validationEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = Presentation.Contracts.Responses.ErrorResponse.CreateError(
                        "VALIDATION_ERROR",
                        validationEx.Message);
                    break;

                case Application.Exceptions.ConflictException conflictEx:
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    response = Presentation.Contracts.Responses.ErrorResponse.CreateError(
                        "CONFLICT",
                        conflictEx.Message);
                    break;

                case Application.Exceptions.AccessDeniedException accessEx:
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    response = Presentation.Contracts.Responses.ErrorResponse.CreateError(
                        "ACCESS_DENIED",
                        accessEx.Message);
                    break;

                case Application.Exceptions.BusinessLogicException businessEx:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response = Presentation.Contracts.Responses.ErrorResponse.CreateError(
                        "BUSINESS_LOGIC_ERROR",
                        businessEx.Message);
                    break;

                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response = Presentation.Contracts.Responses.ErrorResponse.CreateError(
                        "INTERNAL_SERVER_ERROR",
                        "An internal server error occurred. Please try again later.");
                    break;
            }

            return context.Response.WriteAsJsonAsync(response);
        }
    }

    /// <summary>
    /// Middleware for handling correlation IDs for distributed tracing.
    /// Adds a CorrelationId header to track requests across services.
    /// 
    /// Middleware để xử lý các ID tương quan để theo dõi phân tán.
    /// Thêm tiêu đề CorrelationId để theo dõi yêu cầu trên các dịch vụ.
    /// </summary>
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationIdHeader = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Extract or generate correlation ID
            var correlationId = context.Request.Headers.TryGetValue(CorrelationIdHeader, out var headerValue)
                ? headerValue.ToString()
                : Guid.NewGuid().ToString();

            // Add to response headers
            context.Response.Headers.Add(CorrelationIdHeader, correlationId);

            // Store in HttpContext items for use in logging
            context.Items["CorrelationId"] = correlationId;

            await _next(context);
        }
    }
}
