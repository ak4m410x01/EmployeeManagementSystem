using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Domain.Enumerations.Users;

namespace EmployeeManagementSystem.Persistence.DataSeeding.Users
{
    public static class UsersDataSeeding
    {
        public static async Task InitializeUsersAsync(this IUnitOfWork unitOfWork, ISpecification<User> specification)
        {
            await unitOfWork.InitializeEmployeesAsync(specification);
        }

        private static async Task InitializeEmployeesAsync(this IUnitOfWork unitOfWork, ISpecification<User> specification)
        {
            specification.ClearCriteriaExpressions();
            specification.AddCriteriaExpression(u => u.UserTypeId == (int)UserTypes.Employee);
            if (await unitOfWork.Repository<User>().AnyAsync(specification))
                return;

            var users = new List<User>
            {
                new User() {
                    UserTypeId = (int)UserTypes.Admin,
                    Email = "admin@dmj-eg.com",
                    Password = "P@ssw0rd",
                    Name = "Mohamed Fathy",
                    Department = "Founder and CTO",
                },
                new User() {
                    UserTypeId = (int)UserTypes.Employee,
                    Email = "hr@dmj-eg.com",
                    Password = "P@ssw0rd",
                    Name = "Ahmed Ali",
                    Department = "Founder and COO",
                },
                new User() {
                    UserTypeId = (int)UserTypes.Employee,
                    Email = "info@dmj-eg.com",
                    Password = "P@ssw0rd",
                    Name = "Bassem Elsharkawy",
                    Department = "Founder and CEO",
                },
            };

            await unitOfWork.Repository<User>()
                            .AddRangeAsync(users);
        }
    }
}