using EmployeeManagementSystem.Application.Features.Authentication.Queries.SignIn.Requests;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Features.Authentication.Queries.SignIn.Validators
{
    public class SignInQueryValidator : AbstractValidator<SignInQueryRequest>
    {
        #region Constructors

        public SignInQueryValidator()
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
        }

        #endregion Methods
    }
}