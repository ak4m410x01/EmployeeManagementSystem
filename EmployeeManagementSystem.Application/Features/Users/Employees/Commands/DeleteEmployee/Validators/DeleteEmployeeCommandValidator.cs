using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.DeleteEmployee.Requests;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Commands.DeleteEmployee.Validators
{
    public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommandRequest>
    {
        #region Constructors

        public DeleteEmployeeCommandValidator()
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