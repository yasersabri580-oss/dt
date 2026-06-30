using Doctor.Domain.Entities;
using Doctor.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrastructure.Data.Configurations;

public class QualificationConfiguration : IEntityTypeConfiguration<Qualification>
{
    public void Configure(EntityTypeBuilder<Qualification> builder)
    {
        builder.ToTable("qualifications");
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id).UseIdentityColumn();

        builder.Property(q => q.DoctorId).IsRequired();
        builder.Property(q => q.YearLabel).IsRequired().HasMaxLength(20);
        builder.Property(q => q.SortOrder).IsRequired();
        builder.Property(q => q.IsActive).IsRequired();
        builder.Property(q => q.Title).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(q => q.Body).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
    }
}
