using EmployeeManagementSystem.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeManagementSystem.Persistence.EntitiesConfiguration.Users
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            // Table Name Configuration
            builder.ToTable("RefreshTokens");

            // Properties Configuration
            builder.Property(token => token.Token)
                    .IsRequired();

            builder.Property(token => token.ExpiresAt)
                   .IsRequired();

            builder.Property(token => token.IsExpired)
                .HasComputedColumnSql("CASE WHEN [ExpiresAt] <= GETUTCDATE() THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END");

            builder.Property(token => token.RevokedAt)
                   .IsRequired(false);

            builder.Property(token => token.IsActive)
                   .HasComputedColumnSql("CASE WHEN [RevokedAt] IS NULL AND [ExpiresAt] > GETUTCDATE() THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END");

            builder.Property(token => token.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()")
                   .IsRequired();

            // Relationships Configuration
            builder.HasOne(config => config.User)
                   .WithMany(config => config.RefreshTokens)
                   .HasForeignKey(config => config.UserId)
                   .IsRequired();
        }
    }
}