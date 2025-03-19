using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Persistence.DataSeeding.Users;
using EmployeeManagementSystem.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagementSystem.Persistence.DataSeeding
{
    public static class DataSeeding
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var context = services.GetService<EmployeeManagementSystemDbContext>();
            if (context == null)
                return;

            var isPendingMigrations = (await context.Database.GetPendingMigrationsAsync()).Any();
            if (isPendingMigrations)
            {
                await context.Database.MigrateAsync();

                #region Initialize Data

                // Users
                await services.InitializeUserTypesDataAsync();
                await services.InitializeUsersDataAsync();

                #endregion Initialize Data
            }
        }

        private static async Task InitializeUserTypesDataAsync(this IServiceProvider services)
        {
            var unitOfWork = services.GetService<IUnitOfWork>();
            var specification = services.GetService<ISpecification<UserType>>();

            if (unitOfWork == null || specification == null)
                return;

            await unitOfWork.InitializeUserTypesAsync(specification);
        }

        private static async Task InitializeUsersDataAsync(this IServiceProvider services)
        {
            var unitOfWork = services.GetService<IUnitOfWork>();
            var specification = services.GetService<ISpecification<User>>();

            if (unitOfWork == null || specification == null)
                return;

            await unitOfWork.InitializeUsersAsync(specification);
        }
    }
}