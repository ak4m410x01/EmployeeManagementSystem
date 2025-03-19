using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Entities;
using EmployeeManagementSystem.Domain.Enumerations.Users;

namespace EmployeeManagementSystem.Persistence.DataSeeding.Users
{
    public static class UserTypesDataSeeding
    {
        public static async Task InitializeUserTypesAsync(this IUnitOfWork unitOfWork, ISpecification<UserType> specification)
        {
            if (await unitOfWork.Repository<UserType>().AnyAsync(specification))
                return;

            var userTypes = Enum.GetValues(typeof(UserTypes))
                                .Cast<UserTypes>()
                                .Select(type => new UserType { Name = type.ToString() })
                                .ToList();

            await unitOfWork.Repository<UserType>().AddRangeAsync(userTypes);
        }
    }
}