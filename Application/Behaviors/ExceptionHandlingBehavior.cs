using System;

namespace Application.Behaviors
{
    public class ExceptionHandlingBehavior<TRequest, TResponse>
    {
        public TResponse Handle(TRequest request, Func<TRequest, TResponse> next)
        {
            try
            {
                return next(request);
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Exception in {typeof(TRequest).Name}: {ex.Message}");
                throw; // Re-throw or handle as needed
            }
        }
    }
}