using EmployeeManagementSystem.Application.Interfaces.Repositories;
using EmployeeManagementSystem.Application.Interfaces.UnitOfWorks;
using EmployeeManagementSystem.Domain.Common.Abstracts;
using EmployeeManagementSystem.Persistence.DbContexts;
using EmployeeManagementSystem.Persistence.Repositories;
using System.Collections.Concurrent;

namespace EmployeeManagementSystem.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Properties

        private readonly EmployeeManagementSystemDbContext _context;

        private ConcurrentDictionary<string, object> _repositories;

        #endregion Properties

        #region Constructors

        public UnitOfWork(EmployeeManagementSystemDbContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<string, object>();
        }

        #endregion Constructors

        #region Methods

        public IRepository<T> Repository<T>() where T : BaseEntity
        {
            string type = typeof(T).Name;

            Repository<T> repository = new(_context);

            return (IRepository<T>)_repositories.GetOrAdd(type, repository);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        #endregion Methods
    }
}