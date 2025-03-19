using EmployeeManagementSystem.Application.Interfaces.Services.Authentication;
using EmployeeManagementSystem.Infrastructure.Services.Authentication;
using EmployeeManagementSystem.Shared.Handlers.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EmployeeManagementSystem.Infrastructure.Extensions.ServiceCollections.Authentication
{
    public static class AuthenticationServiceCollection
    {
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
        {
            // Token Service
            services.AddOptions<JwtConfiguration>()
                    .BindConfiguration(JwtConfiguration.SectionPath)
                    .ValidateDataAnnotations()
                    .ValidateOnStart();

            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<JwtConfiguration>>().Value);

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}