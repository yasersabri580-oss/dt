
using Doctor.Domain.Entities;
using Doctor.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AboutHighlightConfiguration : IEntityTypeConfiguration<AboutHighlight>
{
    public void Configure(EntityTypeBuilder<AboutHighlight> builder)
    {
        builder.ToTable("about_highlights");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).UseIdentityColumn();

        builder.Property(a => a.DoctorId).IsRequired();
        builder.Property(a => a.SortOrder).IsRequired();
        builder.Property(a => a.IsActive).IsRequired();
        builder.Property(a => a.Title).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(a => a.Body).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
    }
}
