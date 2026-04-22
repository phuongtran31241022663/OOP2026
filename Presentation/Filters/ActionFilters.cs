using Presentation.Contracts.Requests;
using Presentation.Contracts.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Presentation.Filters
{
    /// <summary>
    /// Action filter for validating API request contracts.
    /// Automatically validates IValidatedRequest implementations.
    /// 
    /// Bộ lọc hành động để xác thực các hợp đồng yêu cầu API.
    /// Tự động xác thực các triển khai IValidatedRequest.
    /// </summary>
    public class ValidationFilter : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            // Extract validated request from action arguments
            var validatedRequest = context.ActionArguments.Values
                .FirstOrDefault(x => x is IValidatedRequest) as IValidatedRequest;

            if (validatedRequest != null)
            {
                if (!validatedRequest.Validate(out var errors))
                {
                    var errorDictionary = new Dictionary<string, string[]>
                    {
                        { "ValidationErrors", errors.ToArray() }
                    };

                    var errorResponse = ErrorResponse.CreateValidationError(errorDictionary);

                    context.Result = new BadRequestObjectResult(
                        new ApiResponse
                        {
                            Success = false,
                            Message = "Validation failed",
                            Data = errorResponse
                        });

                    return;
                }
            }

            await next();
        }
    }

    /// <summary>
    /// Action filter for logging API requests and responses.
    /// </summary>
    public class LoggingFilter : Attribute, IAsyncActionFilter
    {
        private readonly ILogger<LoggingFilter> _logger;

        public LoggingFilter(ILogger<LoggingFilter> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var correlationId = context.HttpContext.Items["CorrelationId"]?.ToString() ?? "unknown";
            var actionName = context.ActionDescriptor.DisplayName;

            _logger.LogInformation(
                "Executing action {ActionName} with correlation ID: {CorrelationId}",
                actionName,
                correlationId);

            var result = await next();

            if (result.Exception != null)
            {
                _logger.LogError(
                    result.Exception,
                    "Action {ActionName} failed with correlation ID: {CorrelationId}",
                    actionName,
                    correlationId);
            }
            else
            {
                _logger.LogInformation(
                    "Action {ActionName} completed successfully with correlation ID: {CorrelationId}",
                    actionName,
                    correlationId);
            }
        }
    }
}
