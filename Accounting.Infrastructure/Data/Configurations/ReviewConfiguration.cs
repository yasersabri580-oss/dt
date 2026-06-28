using Accounting.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accounting.Infrastructure.Data.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.ToTable("reviews");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).UseIdentityColumn();

        builder.Property(r => r.DoctorId).IsRequired();
        builder.Property(r => r.PatientName).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Role).IsRequired().HasMaxLength(200);
        builder.Property(r => r.Quote).IsRequired().HasColumnType("nvarchar(max)");
        builder.Property(r => r.Rating).IsRequired();
        builder.Property(r => r.IsActive).IsRequired();
        builder.Property(r => r.SortOrder).IsRequired();
        builder.Property(r => r.CreatedAt).IsRequired();
    }
}
