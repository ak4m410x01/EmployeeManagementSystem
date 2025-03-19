using EmployeeManagementSystem.Application.Behaviors.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EmployeeManagementSystem.Application.Extensions.ServiceCollections
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            // Add AutoMapper Configuration
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Add Validation Configuration
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // Add MediatR Configuration
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            });

            // Add Context Accessor Configuration
            services.AddHttpContextAccessor();

            return services;
        }
    }
}