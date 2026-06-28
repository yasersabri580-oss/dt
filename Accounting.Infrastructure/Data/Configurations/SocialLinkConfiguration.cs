using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Data.Configurations;

public class SocialLinkConfiguration : IEntityTypeConfiguration<SocialLink>
{
    public void Configure(EntityTypeBuilder<SocialLink> builder)
    {
        builder.ToTable("social_links");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).UseIdentityColumn();

        builder.Property(s => s.DoctorId).IsRequired();
        builder.Property(s => s.Label).IsRequired().HasMaxLength(100);
        builder.Property(s => s.Href).IsRequired().HasMaxLength(500);
        builder.Property(s => s.IconName).HasMaxLength(100);   // nullable
        builder.Property(s => s.SortOrder).IsRequired();
        builder.Property(s => s.IsActive).IsRequired();
    }
}
