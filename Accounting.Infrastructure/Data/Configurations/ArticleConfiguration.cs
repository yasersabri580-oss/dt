using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Data.Configurations;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> builder)
    {
        builder.ToTable("articles");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).UseIdentityColumn();

        builder.Property(a => a.DoctorId).IsRequired();
        builder.Property(a => a.Slug).IsRequired().HasMaxLength(300);
        builder.Property(a => a.ReadingMinutes).IsRequired();
        builder.Property(a => a.PublishedAt).IsRequired();
        builder.Property(a => a.CoverUrl).HasMaxLength(1000);   // nullable
        builder.Property(a => a.IsPublished).IsRequired();
        builder.Property(a => a.IsFeatured).IsRequired();
        builder.Property(a => a.CreatedAt).IsRequired();
        builder.Property(a => a.UpdatedAt).IsRequired();

        // jsonb → nvarchar(max)
        builder.Property(a => a.Title).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(a => a.Excerpt).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(a => a.Category).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(a => a.Content).IsRequired().HasColumnType("nvarchar(max)");

        // Unique slug per doctor
        builder.HasIndex(a => new { a.DoctorId, a.Slug }).IsUnique();
    }
}
