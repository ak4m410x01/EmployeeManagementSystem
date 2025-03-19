using EmployeeManagementSystem.Infrastructure.Extensions.ServiceCollections.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementSystem.Infrastructure.Extensions.ServiceCollections
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthenticationService();

            return services;
        }
    }
}