using EmployeeManagementSystem.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementSystem.Persistence.Extensions.ServiceCollections.DbContexts
{
    public static class DbContextsServiceCollection
    {
        public static IServiceCollection AddDbContextsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEmployeeManagementSystemDbContextConfiguration(configuration);

            return services;
        }

        private static IServiceCollection AddEmployeeManagementSystemDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Get Connection String From appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                                       throw new InvalidOperationException("Connection string 'DefaultConnection' is missing.");

            // Add Application Db Context
            services.AddDbContext<EmployeeManagementSystemDbContext>(options =>
            {
                options.UseLazyLoadingProxies()
                       .UseSqlServer(connectionString, builder =>
                       {
                           builder.MigrationsAssembly(typeof(EmployeeManagementSystemDbContext).Assembly.FullName);
                       });
            });
            return services;
        }
    }
}