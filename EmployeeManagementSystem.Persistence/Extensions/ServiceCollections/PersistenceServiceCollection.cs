using EmployeeManagementSystem.Application.Interfaces.Repositories;
using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Persistence.Extensions.ServiceCollections.DbContexts;
using EmployeeManagementSystem.Persistence.Repositories;
using EmployeeManagementSystem.Persistence.Specifications;
using EmployeeManagementSystem.Persistence.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementSystem.Persistence.Extensions.ServiceCollections
{
    public static class PersistenceServiceCollection
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContextsConfiguration(configuration);

            // Specification Configuration
            services.AddTransient(typeof(ISpecification<>), typeof(Specification<>));

            // Repository Configuration
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // UnitOfWork Configuration
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}