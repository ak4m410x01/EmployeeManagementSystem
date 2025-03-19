using EmployeeManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Persistence.EntitiesConfiguration.Users
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            // Table Name Configuration
            builder.ToTable("UserTypes");

            // Properties Configuration
            builder.Property(config => config.Name)
                   .IsRequired();
        }
    }
}