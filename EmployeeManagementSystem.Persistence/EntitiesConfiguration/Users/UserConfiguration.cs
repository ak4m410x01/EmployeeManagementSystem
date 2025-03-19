using EmployeeManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Persistence.EntitiesConfiguration.Users
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Table Name Configuration
            builder.ToTable("Users");

            // Properties Configuration
            builder.Property(config => config.Email)
                   .IsRequired(false);

            builder.Property(config => config.Password)
                   .IsRequired(false);

            builder.Property(config => config.Password)
                   .IsRequired(false);

            builder.Property(config => config.Department)
                   .IsRequired(false);

            builder.Property(config => config.Position)
                   .IsRequired(false);

            builder.Property(config => config.LastLogin)
                   .IsRequired(false);

            builder.Property(config => config.Salary)
                   .IsRequired(false)
                   .HasPrecision(18, 3);

            builder.Property(config => config.JoiningDate)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(config => config.DeletedAt)
                   .IsRequired(false);

            builder.Property(config => config.IsDeleted)
                   .HasComputedColumnSql("CASE WHEN [DeletedAt] IS NULL THEN CAST(0 AS BIT) ELSE CAST(1 AS BIT) END");

            // Relationships Configuration
            builder.HasOne(config => config.UserType)
                   .WithMany(config => config.Users)
                   .HasForeignKey(config => config.UserTypeId)
                   .IsRequired();
        }
    }
}