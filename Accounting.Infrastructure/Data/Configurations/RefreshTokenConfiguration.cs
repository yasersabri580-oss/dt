using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Data.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).UseIdentityColumn();
        builder.Property(r => r.Token).IsRequired().HasMaxLength(500);
        builder.Property(r => r.UserId).IsRequired();
        builder.Property(r => r.ExpiresAt).IsRequired();
        builder.Property(r => r.IsRevoked).HasDefaultValue(false);
        builder.Property(r => r.CreatedAt).IsRequired();

        // Session tracking fields
        builder.Property(r => r.SessionId).IsRequired();
        builder.Property(r => r.DeviceName).HasMaxLength(200);
        builder.Property(r => r.DeviceId).HasMaxLength(200);
        builder.Property(r => r.IpAddress).HasMaxLength(45);   // IPv6 max length
        builder.Property(r => r.UserAgent).HasMaxLength(500);

        builder.HasIndex(r => r.Token).IsUnique();
        builder.HasIndex(r => new { r.UserId, r.SessionId });
    }
}
