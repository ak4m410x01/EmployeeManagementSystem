using EmployeeManagementSystem.Domain.Common.Abstracts;

namespace EmployeeManagementSystem.Domain.Entities.Users;

public class RefreshToken : BaseEntity
{
    #region Properties

    public string? Token { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsExpired { get; private set; }
    public DateTime? RevokedAt { get; set; }
    public bool IsActive { get; private set; }

    #endregion Properties

    #region Relationships

    public int UserId { get; set; }
    public virtual User? User { get; set; }

    #endregion Relationships
}