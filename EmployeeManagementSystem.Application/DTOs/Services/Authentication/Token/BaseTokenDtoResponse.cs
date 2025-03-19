namespace EmployeeManagementSystem.Application.DTOs.Services.Authentication.Token
{
    public class BaseTokenDtoResponse
    {
        #region Properties

        public string? Token { get; set; }
        public DateTime? ExpiresAt { get; set; }

        #endregion Properties
    }
}