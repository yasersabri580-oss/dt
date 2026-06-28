using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Data.Configurations;

public class AchievementConfiguration : IEntityTypeConfiguration<Achievement>
{
    public void Configure(EntityTypeBuilder<Achievement> builder)
    {
        builder.ToTable("achievements");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).UseIdentityColumn();

        builder.Property(a => a.DoctorId).IsRequired();
        builder.Property(a => a.SortOrder).IsRequired();
        builder.Property(a => a.IsActive).IsRequired();
        builder.Property(a => a.Text).IsRequired().HasColumnType("nvarchar(max)");
    }
}
