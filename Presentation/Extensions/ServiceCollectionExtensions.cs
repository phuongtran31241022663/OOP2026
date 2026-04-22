using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Filters;
using Presentation.Middleware;
using System;

namespace Presentation.Extensions
{
    /// <summary>
    /// Extension methods for IServiceCollection to register Presentation layer services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Presentation layer services to the dependency injection container.
        /// Registers controllers, filters, and other presentation-specific services.
        /// </summary>
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            // Add Controllers
            services.AddControllers(options =>
            {
                // Add validation filter globally
                // options.Filters.Add<ValidationFilter>();
                // options.Filters.Add<LoggingFilter>();
            });

            // Add CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });

                options.AddPolicy("AllowLocalhost", builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "http://localhost:5000", "https://localhost:5001")
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Add API versioning (optional)
            // services.AddApiVersioning(options =>
            // {
            //     options.DefaultApiVersion = new ApiVersion(1, 0);
            //     options.AssumeDefaultVersionWhenUnspecified = true;
            //     options.ReportApiVersions = true;
            // });

            // Add Swagger/OpenAPI
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Taxi Booking API",
                    Version = "v1",
                    Description = "API for managing taxi bookings and driver assignments"
                });
            });

            return services;
        }

        /// <summary>
        /// Adds HTTP context accessor for accessing current request context.
        /// </summary>
        public static IServiceCollection AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            return services;
        }
    }

    /// <summary>
    /// Extension methods for WebApplication to configure Presentation layer middleware.
    /// </summary>
    public static class WebApplicationExtensions
    {
        /// <summary>
        /// Configures the HTTP request pipeline with Presentation middleware.
        /// Adds exception handling, correlation IDs, CORS, and Swagger.
        /// </summary>
        public static WebApplication UsePresentationMiddleware(this WebApplication app)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            // Exception handling middleware (should be first)
            app.UseMiddleware<ExceptionMiddleware>();

            // Correlation ID middleware
            app.UseMiddleware<CorrelationIdMiddleware>();

            // HTTPS redirection
            app.UseHttpsRedirection();

            // Swagger/OpenAPI
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Taxi Booking API V1");
                    c.RoutePrefix = string.Empty; // Serve at root
                });
            }

            // CORS
            app.UseCors("AllowLocalhost");

            // Authentication & Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Routing
            app.UseRouting();

            // Map endpoints
            app.MapControllers();

            // Health checks endpoint
            app.MapHealthChecks("/health");

            return app;
        }

        /// <summary>
        /// Maps endpoint configurations with proper versioning and documentation.
        /// </summary>
        public static WebApplication MapEndpointConfigurations(this WebApplication app)
        {
            // Map all endpoints from endpoint definitions
            // app.MapDriverEndpoints();
            // app.MapPassengerEndpoints();
            // app.MapTripEndpoints();
            // app.MapPaymentEndpoints();
            // app.MapAdminEndpoints();

            return app;
        }
    }
}
