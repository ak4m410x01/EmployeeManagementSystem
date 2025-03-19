using EmployeeManagementSystem.Application.DTOs.Services.Authentication.Token;

namespace EmployeeManagementSystem.Application.DTOs.Services.Authentication.SignIn
{
    public class SignInDtoResponse
    {
        #region Properties

        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public AccessTokenDtoResponse? AccessToken { get; set; }
        public RefreshTokenDtoResponse? RefreshToken { get; set; }

        #endregion Properties
    }
}