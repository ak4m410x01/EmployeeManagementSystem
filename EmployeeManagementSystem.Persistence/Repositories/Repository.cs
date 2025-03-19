using EmployeeManagementSystem.Application.Interfaces.Repositories;
using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Domain.Common.Abstracts;
using EmployeeManagementSystem.Persistence.DbContexts;
using EmployeeManagementSystem.Persistence.Specifications.Evaluations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace EmployeeManagementSystem.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        #region Properties

        protected readonly EmployeeManagementSystemDbContext _context;

        #endregion Properties

        #region Constructors

        public Repository(EmployeeManagementSystemDbContext context)
        {
            _context = context;
        }

        #endregion Constructors

        #region Method

        protected virtual DbSet<T> GetDbSet()
        {
            return _context.Set<T>();
        }

        protected virtual IQueryable<T> GetQuery(ISpecification<T> specification)
        {
            return SpecificationEvaluator<T>.GetQuery(GetDbSet(), specification);
        }

        #region Find All

        public virtual Task<IQueryable<T>> FindAllAsync(ISpecification<T> specification, CancellationToken token = default)
        {
            return Task.FromResult(GetQuery(specification));
        }

        public virtual Task<IQueryable<T>> FindAllAsNoTrackingAsync(ISpecification<T> specification, CancellationToken token = default)
        {
            return Task.FromResult(GetQuery(specification).AsNoTracking());
        }

        #endregion Find All

        #region Find

        public virtual Task<T?> FindAsync(ISpecification<T> specification, CancellationToken token = default)
        {
            return GetQuery(specification).FirstOrDefaultAsync(token);
        }

        public virtual Task<T?> FindAsNoTrackingAsync(ISpecification<T> specification, CancellationToken token = default)
        {
            return GetQuery(specification).AsNoTracking().FirstOrDefaultAsync(token);
        }

        #endregion Find

        #region IsExists

        public virtual async Task<bool> IsExistsAsync(ISpecification<T> specification, CancellationToken token = default)
        {
            return (await GetQuery(specification).AsNoTracking().FirstOrDefaultAsync(token)) != null;
        }

        public virtual async Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken token = default)
        {
            return (await GetQuery(specification).AsNoTracking().AnyAsync(token));
        }

        #endregion IsExists

        #region IsModified

        public async Task<bool> IsModifiedAsync(T entity)
        {
            var entry = _context.Entry(entity);

            // Check if entity state is 'Modified' or any property is modified
            return await Task.FromResult(entry.State == EntityState.Modified || entry.Properties.Any(p => p.IsModified));
        }

        #endregion IsModified

        #region Add

        public virtual async Task AddAsync(T entity, CancellationToken token = default)
        {
            await GetDbSet().AddAsync(entity, token);
            await SaveChangesAsync(token);
        }

        public virtual async Task AddRangeAsync(ICollection<T> entities, CancellationToken token = default)
        {
            await GetDbSet().AddRangeAsync(entities, token);
            await SaveChangesAsync(token);
        }

        #endregion Add

        #region Update

        public virtual async Task UpdateAsync(T entity, CancellationToken token = default)
        {
            GetDbSet().Update(entity);
            await SaveChangesAsync(token);
        }

        public virtual async Task UpdateRangeAsync(ICollection<T> entities, CancellationToken token = default)
        {
            GetDbSet().UpdateRange(entities);
            await SaveChangesAsync(token);
        }

        #endregion Update

        #region Delete

        public virtual async Task DeleteAsync(T entity, CancellationToken token = default)
        {
            GetDbSet().Remove(entity);
            await SaveChangesAsync(token);
        }

        public virtual async Task DeleteRangeAsync(ICollection<T> entities, CancellationToken token = default)
        {
            GetDbSet().RemoveRange(entities);
            await SaveChangesAsync(token);
        }

        #endregion Delete

        #region Save

        public virtual async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            return await _context.SaveChangesAsync(token);
        }

        #endregion Save

        #region Transaction

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = default)
        {
            return await _context.Database.BeginTransactionAsync(token);
        }

        public virtual async Task CommitTransactionAsync(CancellationToken token = default)
        {
            await _context.Database.CommitTransactionAsync(token);
        }

        public virtual async Task RollbackTransactionAsync(CancellationToken token = default)
        {
            await _context.Database.RollbackTransactionAsync(token);
        }

        #endregion Transaction

        #region Attach

        public virtual async Task AttachAsync(T entity, CancellationToken token = default)
        {
            GetDbSet().Attach(entity);
            await SaveChangesAsync(token);
        }

        public virtual async Task AttachRangeAsync(ICollection<T> entities, CancellationToken token = default)
        {
            GetDbSet().AttachRange(entities);
            await SaveChangesAsync(token);
        }

        #endregion Attach

        #region Min & Max

        public async Task<TResult?> MinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken token = default) where TResult : struct
        {
            return await GetDbSet().MinAsync(selector, token);
        }

        public async Task<TResult?> MinAsync<TResult>(Expression<Func<T, TResult>> selector, ISpecification<T> specification, CancellationToken token = default) where TResult : struct
        {
            var items = await FindAllAsNoTrackingAsync(specification);
            return await items.MinAsync(selector, token);
        }

        public async Task<TResult?> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken token = default) where TResult : struct
        {
            return await GetDbSet().MaxAsync(selector, token);
        }

        public async Task<TResult?> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, ISpecification<T> specification, CancellationToken token = default) where TResult : struct
        {
            var items = await FindAllAsNoTrackingAsync(specification);
            return await items.MaxAsync(selector, token);
        }

        #endregion Min & Max

        #region Sql

        public async Task<string> GetTableName()
        {
            var entityType = _context.Model.FindEntityType(typeof(T));
            return await Task.FromResult(entityType?.GetTableName() ?? string.Empty);
        }

        public async Task<int> ExecuteSqlAsync(string sql, params object[] parameters)
        {
            return await _context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        #endregion Sql

        #endregion Method
    }
}