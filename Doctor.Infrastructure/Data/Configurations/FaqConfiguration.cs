using Doctor.Domain.Entities;
using Doctor.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrastructure.Data.Configurations;

public class FaqConfiguration : IEntityTypeConfiguration<Faq>
{
    public void Configure(EntityTypeBuilder<Faq> builder)
    {
        builder.ToTable("faqs");
        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).UseIdentityColumn();

        builder.Property(f => f.DoctorId).IsRequired();
        builder.Property(f => f.SortOrder).IsRequired();
        builder.Property(f => f.IsActive).IsRequired();
        builder.Property(f => f.Question).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
        builder.Property(f => f.Answer).IsRequired().HasColumnType("nvarchar(max)").HasLocalizedJson();
    }
}
