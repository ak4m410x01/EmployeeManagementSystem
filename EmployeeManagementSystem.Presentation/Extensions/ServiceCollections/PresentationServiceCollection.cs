using EmployeeManagementSystem.Presentation.Extensions.ServiceCollections.ApiVersioning;
using EmployeeManagementSystem.Presentation.Extensions.ServiceCollections.Authentication;
using EmployeeManagementSystem.Presentation.Extensions.ServiceCollections.Authorization;
using EmployeeManagementSystem.Presentation.Extensions.ServiceCollections.Cors;
using EmployeeManagementSystem.Shared.Responses;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EmployeeManagementSystem.Presentation.Extensions.ServiceCollections
{
    public static class PresentationServiceCollection
    {
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services, IConfiguration configuration, IHostBuilder host)
        {
            // 1. Add Controller Services (MVC controllers for handling HTTP requests)
            services.AddControllers();

            services.AddOpenApi();

            // 2. Register Response Handler (centralized handling for API responses)
            services.AddSingleton<ResponseHandler>();

            // 3. Configure API Versioning (support for multiple API versions)
            services.AddApiVersioningConfiguration();

            // 4. Add CORS Configuration (Cross-Origin Resource Sharing policies)
            services.AddCorsConfiguration();

            // 5. Add Authentication Services (user authentication setup)
            services.AddAuthenticationConfiguration(configuration);

            // 6. Add Authorization Services (user authorization setup)
            services.AddAuthorizationConfiguration();

            // 7. Add Serilog Logger Configuration (application logging with Serilog)
            host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value?.Errors.Count > 0)
                        .Select(e => e.Key.Split('.').Last())
                        .Where(name => name != "request")
                        .Distinct()
                        .ToList();

                    var response = new
                    {
                        Message = "Invalid request data. The following fields have incorrect data types:",
                        InvalidFields = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            services.AddOpenApi();

            return services;
        }
    }
}