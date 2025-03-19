using EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindAllEmployees.Requests;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Queries.FindAllEmployees.Validators
{
    public class FindAllEmployeesQueryValidator : AbstractValidator<FindAllEmployeesQueryRequest>
    {
        #region Constructors

        public FindAllEmployeesQueryValidator()
        {
            InitializeRules();
        }

        #endregion Constructors

        #region Methods

        private void InitializeRules()
        {
            RuleFor(request => request.PageNumber)
                .GreaterThan(0);

            RuleFor(request => request.PageSize)
                .GreaterThan(0);
        }

        #endregion Methods
    }
}