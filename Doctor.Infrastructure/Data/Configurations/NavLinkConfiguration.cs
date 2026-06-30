using Doctor.Domain.Entities;
using Doctor.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrastructure.Data.Configurations;

public class NavLinkConfiguration : IEntityTypeConfiguration<NavLink>
{
    public void Configure(EntityTypeBuilder<NavLink> builder)
    {
        builder.ToTable("nav_links");
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Id).UseIdentityColumn();

        builder.Property(n => n.DoctorId).IsRequired();
        builder.Property(n => n.Href).IsRequired().HasMaxLength(500);
        builder.Property(n => n.SortOrder).IsRequired();
        builder.Property(n => n.IsActive).IsRequired();
        builder.Property(n => n.Label).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
    }
}
