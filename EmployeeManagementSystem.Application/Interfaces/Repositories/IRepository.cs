using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Domain.Common.Abstracts;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace EmployeeManagementSystem.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        #region Find All

        Task<IQueryable<T>> FindAllAsync(ISpecification<T> specification, CancellationToken token = default);

        Task<IQueryable<T>> FindAllAsNoTrackingAsync(ISpecification<T> specification, CancellationToken token = default);

        #endregion Find All

        #region Find

        Task<T?> FindAsync(ISpecification<T> specification, CancellationToken token = default);

        Task<T?> FindAsNoTrackingAsync(ISpecification<T> specification, CancellationToken token = default);

        #endregion Find

        #region IsExists

        Task<bool> IsExistsAsync(ISpecification<T> specification, CancellationToken token = default);

        Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken token = default);

        #endregion IsExists

        #region Is Modified

        Task<bool> IsModifiedAsync(T entity);

        #endregion Is Modified

        #region Add

        Task AddAsync(T entity, CancellationToken cancellationToke = default);

        Task AddRangeAsync(ICollection<T> entities, CancellationToken token = default);

        #endregion Add

        #region Update

        Task UpdateAsync(T entity, CancellationToken token = default);

        Task UpdateRangeAsync(ICollection<T> entities, CancellationToken token = default);

        #endregion Update

        #region Delete

        Task DeleteAsync(T entity, CancellationToken token = default);

        Task DeleteRangeAsync(ICollection<T> entities, CancellationToken token = default);

        #endregion Delete

        #region Save

        Task<int> SaveChangesAsync(CancellationToken token = default);

        #endregion Save

        #region Transaction

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken token = default);

        Task CommitTransactionAsync(CancellationToken token = default);

        Task RollbackTransactionAsync(CancellationToken token = default);

        #endregion Transaction

        #region Attach

        Task AttachAsync(T entity, CancellationToken token = default);

        Task AttachRangeAsync(ICollection<T> entities, CancellationToken token = default);

        #endregion Attach

        #region Min && Max

        Task<TResult?> MinAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken token = default) where TResult : struct;

        Task<TResult?> MinAsync<TResult>(Expression<Func<T, TResult>> selector, ISpecification<T> specification, CancellationToken token = default) where TResult : struct;

        Task<TResult?> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, CancellationToken token = default) where TResult : struct;

        Task<TResult?> MaxAsync<TResult>(Expression<Func<T, TResult>> selector, ISpecification<T> specification, CancellationToken token = default) where TResult : struct;

        #endregion Min && Max

        #region Sql

        Task<string> GetTableName();

        Task<int> ExecuteSqlAsync(string sql, params object[] parameters);

        #endregion Sql
    }
}