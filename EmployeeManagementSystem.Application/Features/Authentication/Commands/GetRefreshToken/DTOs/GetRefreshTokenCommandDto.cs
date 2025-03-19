namespace EmployeeManagementSystem.Application.Features.Authentication.Commands.GetRefreshToken.DTOs
{
    public class GetRefreshTokenCommandDto
    {
        #region Properties

        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiresAt { get; set; }

        #endregion Properties
    }
}