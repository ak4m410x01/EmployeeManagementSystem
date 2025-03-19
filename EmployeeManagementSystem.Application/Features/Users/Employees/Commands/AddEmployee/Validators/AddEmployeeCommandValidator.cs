using EmployeeManagementSystem.Application.Features.Users.Employees.Commands.AddEmployee.Requests;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Features.Users.Employees.Commands.AddEmployee.Validators
{
    public class AddEmployeeCommandValidator : AbstractValidator<AddEmployeeCommandRequest>
    {
        #region Constructors

        public AddEmployeeCommandValidator()
        {
            InitializeRules();
        }

        #endregion Constructors

        #region Methods

        private void InitializeRules()
        {
            RuleFor(request => request.Email)
               .NotEmpty().WithMessage("Email is required.")
               .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Email is not in a valid format.");

            RuleFor(request => request.Password)
                .NotEmpty();

            RuleFor(request => request.Name)
                .NotEmpty();

            RuleFor(request => request.Salary)
                .NotEmpty()
                .GreaterThan(0);
        }

        #endregion Methods
    }
}