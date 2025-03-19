using EmployeeManagementSystem.Domain.Common.Abstracts;

namespace EmployeeManagementSystem.Domain.Entities
{
    public class UserType : BaseEntity
    {
        #region Properties

        public string? Name { get; set; }

        #endregion Properties

        #region Relationships

        public virtual ICollection<User>? Users { get; set; }

        #endregion Relationships
    }
}