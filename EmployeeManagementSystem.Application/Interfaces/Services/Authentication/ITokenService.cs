using EmployeeManagementSystem.Application.DTOs.Services.Authentication.Token;
using EmployeeManagementSystem.Domain.Entities;

namespace EmployeeManagementSystem.Application.Interfaces.Services.Authentication
{
    public interface ITokenService
    {
        Task<AccessTokenDtoResponse> GenerateAccessTokenAsync(User user);

        Task<RefreshTokenDtoResponse> GenerateRefreshTokenAsync(User user, bool revokeOld = false);
    }
}