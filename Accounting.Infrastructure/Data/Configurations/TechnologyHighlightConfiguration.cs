using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Data.Configurations;

public class TechnologyHighlightConfiguration : IEntityTypeConfiguration<TechnologyHighlight>
{
    public void Configure(EntityTypeBuilder<TechnologyHighlight> builder)
    {
        builder.ToTable("technology_highlights");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).UseIdentityColumn();

        builder.Property(t => t.DoctorId).IsRequired();
        builder.Property(t => t.SortOrder).IsRequired();
        builder.Property(t => t.IsActive).IsRequired();
        builder.Property(t => t.Title).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(t => t.Body).IsRequired().HasColumnType("nvarchar(max)");
    }
}
