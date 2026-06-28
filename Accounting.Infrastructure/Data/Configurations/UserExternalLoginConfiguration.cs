using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Data.Configurations;

public class UserExternalLoginConfiguration : IEntityTypeConfiguration<UserExternalLogin>
{
    public void Configure(EntityTypeBuilder<UserExternalLogin> builder)
    {
        builder.ToTable("UserExternalLogins");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).UseIdentityColumn();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.Provider).IsRequired().HasMaxLength(50);
        builder.Property(e => e.ProviderUserId).IsRequired().HasMaxLength(256);
        builder.Property(e => e.Email).HasMaxLength(256);
        builder.Property(e => e.DisplayName).HasMaxLength(256);
        builder.Property(e => e.CreatedAt).IsRequired();

        // Composite unique index: one entry per provider per user per provider account.
        builder.HasIndex(e => new { e.Provider, e.ProviderUserId }).IsUnique();
    }
}
