using EmployeeManagementSystem.Domain.Common.Abstracts;
using EmployeeManagementSystem.Domain.Entities.Users;

namespace EmployeeManagementSystem.Domain.Entities
{
    public class User : BaseEntity
    {
        #region Properties

        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }
        public DateTime JoiningDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? DeletedAt { get; set; }
        public bool IsDeleted { get; set; }

        #endregion Properties

        #region Relationships

        public int UserTypeId { get; set; }
        public virtual UserType? UserType { get; set; }
        public virtual ICollection<RefreshToken>? RefreshTokens { get; set; }

        #endregion Relationships
    }
}