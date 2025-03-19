using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindEmployeeById.Requests;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindEmployeeById.Validators
{
    public class FindEmployeeByIdQueryValidator : AbstractValidator<FindEmployeeByIdQueryRequest>
    {
        #region Constructors

        public FindEmployeeByIdQueryValidator()
        {
            InitializeRules();
        }

        #endregion Constructors

        #region Methods

        private void InitializeRules()
        {
            RuleFor(request => request.Id)
                .NotEmpty()
                .GreaterThan(0);
        }

        #endregion Methods
    }
}