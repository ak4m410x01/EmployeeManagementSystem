using EmployeeManagementSystem.Application.DTOs.Services.Authentication.SignIn;
using EmployeeManagementSystem.Application.DTOs.Services.Authentication.Token;

namespace EmployeeManagementSystem.Application.Interfaces.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<SignInDtoResponse> SignInAsync(string email, string password);

        Task<AccessTokenDtoResponse> GetAccessTokenAsync(string refreshToken);

        Task<RefreshTokenDtoResponse> GetRefreshTokenAsync(string refreshToken);
    }
}