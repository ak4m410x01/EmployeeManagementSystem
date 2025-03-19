using EmployeeManagementSystem.Application.Interfaces.Repositories;
using EmployeeManagementSystem.Domain.Common.Abstracts;

namespace EmployeeManagementSystem.Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<T> Repository<T>() where T : BaseEntity;

        Task<int> SaveAsync();
    }
}