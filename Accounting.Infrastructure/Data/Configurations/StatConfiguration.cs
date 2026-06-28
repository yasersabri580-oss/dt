using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Data.Configurations;

public class StatConfiguration : IEntityTypeConfiguration<Stat>
{
    public void Configure(EntityTypeBuilder<Stat> builder)
    {
        builder.ToTable("stats");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).UseIdentityColumn();

        builder.Property(s => s.DoctorId).IsRequired();
        builder.Property(s => s.Value).IsRequired();
        builder.Property(s => s.Suffix).HasMaxLength(20);   // nullable by default
        builder.Property(s => s.SortOrder).IsRequired();
        builder.Property(s => s.IsActive).IsRequired();
        builder.Property(s => s.Label).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(s => s.Note).IsRequired().HasColumnType("nvarchar(max)");
    }
}
