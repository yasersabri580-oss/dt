using Doctor.Domain.Entities;
using Doctor.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrastructure.Data.Configurations;

public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
{
    public void Configure(EntityTypeBuilder<ContactInfo> builder)
    {
        builder.ToTable("contact_info");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).UseIdentityColumn();

        builder.Property(c => c.DoctorId).IsRequired();
        builder.HasIndex(c => c.DoctorId).IsUnique();   // one-to-one side

        builder.Property(c => c.PhoneDisplay).IsRequired().HasMaxLength(50);
        builder.Property(c => c.PhoneLink).IsRequired().HasMaxLength(50);
        builder.Property(c => c.WhatsappUrl).IsRequired().HasMaxLength(500);
        builder.Property(c => c.UpdatedAt).IsRequired();
        builder.Property(c => c.Address).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
    }
}
