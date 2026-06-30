using Doctor.Domain.Entities;
using Doctor.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrastructure.Data.Configurations;

public class HeroImageConfiguration : IEntityTypeConfiguration<HeroImage>
{
    public void Configure(EntityTypeBuilder<HeroImage> builder)
    {
        builder.ToTable("hero_images");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).UseIdentityColumn();

        builder.Property(h => h.DoctorId).IsRequired();
        builder.Property(h => h.KeyName).IsRequired().HasMaxLength(200);
        builder.Property(h => h.StorageUrl).IsRequired().HasMaxLength(1000);
        builder.Property(h => h.SortOrder).IsRequired();
        builder.Property(h => h.IsActive).IsRequired();
        builder.Property(h => h.AltText).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
    }
}
