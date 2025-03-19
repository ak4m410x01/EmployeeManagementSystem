using EmployeeManagementSystem.Application.Features.Authentication.Queries.GetAccessToken.Requests;
using FluentValidation;

namespace EmployeeManagementSystem.Application.Features.Authentication.Queries.GetAccessToken.Validators
{
    public class GetAccessTokenQueryValidator : AbstractValidator<GetAccessTokenQueryRequest>
    {
        #region Constructors

        public GetAccessTokenQueryValidator()
        {
            InitializeRules();
        }

        #endregion Constructors

        #region Methods

        private void InitializeRules()
        {
            // Refresh Token Validator
            RuleFor(request => request.RefreshToken)
                .NotEmpty();
        }

        #endregion Methods
    }
}