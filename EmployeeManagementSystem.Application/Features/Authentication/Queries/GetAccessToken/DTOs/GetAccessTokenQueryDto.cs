namespace EmployeeManagementSystem.Application.Features.Authentication.Queries.GetAccessToken.DTOs
{
    public class GetAccessTokenQueryDto
    {
        #region Properties

        public string? AccessToken { get; set; }
        public DateTime AccessTokenExpiresAt { get; set; }

        #endregion Properties
    }
}