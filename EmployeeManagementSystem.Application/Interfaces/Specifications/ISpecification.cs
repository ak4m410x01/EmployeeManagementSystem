using EmployeeManagementSystem.Domain.Common.Abstracts;
using System.Linq.Expressions;

namespace EmployeeManagementSystem.Application.Interfaces.Specifications
{
    public interface ISpecification<T> where T : BaseEntity
    {
        List<Expression<Func<T, bool>>> CriteriaExpressions { get; }
        List<Expression<Func<T, object>>> IncludeExpressions { get; }
        List<(Expression<Func<T, object>>, bool)> OrderByExpressions { get; }

        void AddCriteriaExpression(Expression<Func<T, bool>> criteriaExpression);

        void ClearCriteriaExpressions();

        void AddIncludeExpression(Expression<Func<T, object>> includeExpression);

        void ClearIncludeExpressions();

        void AddOrderByExpression(Expression<Func<T, object>> orderByExpression, bool ascending = true);

        void ClearOrderByExpressions();

        string GetQueryString(IQueryable<T> query);
    }
}