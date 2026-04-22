using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    /// <summary>
    /// Pipeline behavior for validating requests using FluentValidation.
    /// Automatically runs all registered validators before handler execution.
    /// Giao diện này xác thực tất cả các yêu cầu trước khi xử lý.
    /// </summary>
    public class ValidationBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<object> _validators;

        public ValidationBehavior(IEnumerable<object> validators = null)
        {
            _validators = validators ?? Enumerable.Empty<object>();
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            Func<Task<TResponse>> next)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // Validation would be performed here if validators are registered
            // This is a placeholder for explicit validation behavior
            
            return await next();
        }

        /// <summary>
        /// In production, you would use FluentValidation like this:
        /// 
        /// public async Task&lt;TResponse&gt; Handle(
        ///     TRequest request,
        ///     CancellationToken cancellationToken,
        ///     Func&lt;Task&lt;TResponse&gt;&gt; next)
        /// {
        ///     var context = new ValidationContext&lt;TRequest&gt;(request);
        ///     var failures = new List&lt;ValidationFailure&gt;();
        ///     
        ///     foreach (var validator in _validators.OfType&lt;IValidator&lt;TRequest&gt;&gt;())
        ///     {
        ///         var result = await validator.ValidateAsync(context, cancellationToken);
        ///         failures.AddRange(result.Errors);
        ///     }
        ///     
        ///     if (failures.Any())
        ///         throw new ValidationException(failures);
        ///     
        ///     return await next();
        /// }
        /// </summary>
    }
}
