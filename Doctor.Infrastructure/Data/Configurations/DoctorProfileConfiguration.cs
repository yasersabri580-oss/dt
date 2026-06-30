using Doctor.Domain.Entities;
using Doctor.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrastructure.Data.Configurations;

public class DoctorProfileConfiguration : IEntityTypeConfiguration<DoctorProfile>
{
    public void Configure(EntityTypeBuilder<DoctorProfile> builder)
    {
        builder.ToTable("doctor_profile");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).UseIdentityColumn();

        builder.Property(p => p.DoctorId).IsRequired();
        builder.HasIndex(p => p.DoctorId).IsUnique();

        builder.Property(p => p.ExperienceYears).IsRequired();
        builder.Property(p => p.OgLocale).HasMaxLength(20);
        builder.Property(p => p.UpdatedAt).IsRequired();

        // ── jsonb → nvarchar(max) ──────────────────────────────────────────
        builder.Property(p => p.FullName).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.BrandSubline).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.Tagline).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.HeroTitle).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.HeroCopy).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.PrimaryCta).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.SecondaryCta).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.Mission).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.AboutParagraph).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.Schedule).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.FooterCopy).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.SeoTitle).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.SeoTitleTemplate).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.SeoDescription).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.SeoKeywords).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.OgTitle).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(p => p.OgDescription).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
    }
}
