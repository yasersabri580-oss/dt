using Doctor.Domain.Entities;
using Doctor.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrastructure.Data.Configurations;

public class AppointmentOptionConfiguration : IEntityTypeConfiguration<AppointmentOption>
{
    public void Configure(EntityTypeBuilder<AppointmentOption> builder)
    {
        builder.ToTable("appointment_options");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).UseIdentityColumn();

        builder.Property(a => a.DoctorId).IsRequired();
        builder.Property(a => a.IconName).IsRequired().HasMaxLength(100);
        builder.Property(a => a.SortOrder).IsRequired();
        builder.Property(a => a.IsActive).IsRequired();
        builder.Property(a => a.Title).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(a => a.Body).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
    }
}
