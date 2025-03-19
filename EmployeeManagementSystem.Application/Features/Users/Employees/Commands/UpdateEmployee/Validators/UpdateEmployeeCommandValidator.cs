using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.UpdateEmployee.Requests;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Commands.UpdateEmployee.Validators
{
    internal class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommandRequest>
    {
        #region Constructors

        public UpdateEmployeeCommandValidator()
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

            RuleFor(request => request.Salary)
                .GreaterThan(0);
        }

        #endregion Methods
    }
}