using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Data.Configurations;

public class DoctorServiceConfiguration : IEntityTypeConfiguration<DoctorService>
{
    public void Configure(EntityTypeBuilder<DoctorService> builder)
    {
        builder.ToTable("services");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).UseIdentityColumn();

        builder.Property(s => s.DoctorId).IsRequired();
        builder.Property(s => s.IconName).IsRequired().HasMaxLength(100);
        builder.Property(s => s.SortOrder).IsRequired();
        builder.Property(s => s.IsActive).IsRequired();
        builder.Property(s => s.Title).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(s => s.Body).IsRequired().HasColumnType("nvarchar(max)");
    }
}
