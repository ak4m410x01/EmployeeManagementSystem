using EmployeeManagementSystem.Application.Interfaces.Specifications;
using EmployeeManagementSystem.Domain.Common.Abstracts;
using EmployeeManagementSystem.Persistence.Specifications.Evaluations;
using System.Linq.Expressions;

namespace EmployeeManagementSystem.Persistence.Specifications
{
    public class Specification<T> : ISpecification<T> where T : BaseEntity
    {
        #region Fields

        private List<Expression<Func<T, bool>>> _criteriaExpressions { get; } = new();
        private List<Expression<Func<T, object>>> _includeExpressions { get; } = new();
        private List<(Expression<Func<T, object>>, bool)> _orderByExpressions { get; } = new();

        #endregion Fields

        #region Properties

        public List<Expression<Func<T, bool>>> CriteriaExpressions => _criteriaExpressions;
        public List<Expression<Func<T, object>>> IncludeExpressions => _includeExpressions;
        public List<(Expression<Func<T, object>>, bool)> OrderByExpressions => _orderByExpressions;

        #endregion Properties

        #region Constructors

        public Specification()
        {
        }

        #endregion Constructors

        #region Methods

        public virtual void AddCriteriaExpression(Expression<Func<T, bool>> criteriaExpression)
        {
            _criteriaExpressions.Add(criteriaExpression);
        }

        public virtual void ClearCriteriaExpressions()
        {
            _criteriaExpressions.Clear();
        }

        public virtual void AddIncludeExpression(Expression<Func<T, object>> includeExpression)
        {
            _includeExpressions.Add(includeExpression);
        }

        public virtual void ClearIncludeExpressions()
        {
            _includeExpressions.Clear();
        }

        public virtual void AddOrderByExpression(Expression<Func<T, object>> orderByExpression, bool ascending = true)
        {
            _orderByExpressions.Add((orderByExpression, ascending));
        }

        public virtual void ClearOrderByExpressions()
        {
            _orderByExpressions.Clear();
        }

        public virtual string GetQueryString(IQueryable<T> query)
        {
            return SpecificationEvaluator<T>.GetQueryString(query);
        }

        #endregion Methods
    }
}