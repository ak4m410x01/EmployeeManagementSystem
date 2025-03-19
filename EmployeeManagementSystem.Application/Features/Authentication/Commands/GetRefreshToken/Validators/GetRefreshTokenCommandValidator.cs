using EmployeeManagementSystem.Application.Features.Authentication.Commands.GetRefreshToken.Requests;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Features.Authentication.Commands.GetRefreshToken.Validators
{
    public class GetRefreshTokenCommandValidator : AbstractValidator<GetRefreshTokenCommandRequest>
    {
        #region Constructors

        public GetRefreshTokenCommandValidator()
        {
            InitializeRules();
        }

        #endregion Constructors

        #region Methods

        private void InitializeRules()
        {
            RuleFor(request => request.RefreshToken)
                .NotEmpty();
        }

        #endregion Methods
    }
}