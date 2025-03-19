using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Shared.Handlers.Authentication
{
    public class JwtConfiguration
    {
        #region Properties

        public static string SectionPath { get; set; } = "Authentication:JwtConfiguration";

        [Required]
        public string? Issuer { get; set; }

        [Required]
        public string? Audience { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int AccessTokenExpiryDays { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int RefreshTokenExpiryDays { get; set; }

        [Required]
        public string? Key { get; set; }

        #endregion Properties
    }
}