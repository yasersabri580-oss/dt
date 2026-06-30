using Doctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Doctor.Infrastructure.Data.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor.Domain.Entities.Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor.Domain.Entities.Doctor> builder)
    {
        builder.ToTable("doctors");
        builder.HasKey(d => d.Id);

        // Guid PK — generate on the database side for SQL Server
        builder.Property(d => d.Id)
               .HasDefaultValueSql("NEWID()");

        builder.Property(d => d.Slug)
               .IsRequired()
               .HasColumnType("nvarchar(max)").HasLocalizedJson();

        

        builder.Property(d => d.IsActive).IsRequired();
        builder.Property(d => d.CreatedAt).IsRequired();


        builder.HasOne(d => d.User)
          .WithOne(u => u.doctor)
          .HasForeignKey<Doctor.Domain.Entities.Doctor>(d => d.UserId);

        // ── Relationships ──────────────────────────────────────────────────
        builder.HasOne(d => d.Profile)
               .WithOne(p => p.Doctor)
               .HasForeignKey<DoctorProfile>(p => p.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(d => d.ContactInfo)
               .WithOne(c => c.Doctor)
               .HasForeignKey<ContactInfo>(c => c.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.NavLinks)
               .WithOne(n => n.Doctor)
               .HasForeignKey(n => n.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.AboutHighlights)
               .WithOne(a => a.Doctor)
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Services)
               .WithOne(s => s.Doctor)
               .HasForeignKey(s => s.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.TechnologyHighlights)
               .WithOne(t => t.Doctor)
               .HasForeignKey(t => t.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Stats)
               .WithOne(s => s.Doctor)
               .HasForeignKey(s => s.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Qualifications)
               .WithOne(q => q.Doctor)
               .HasForeignKey(q => q.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Achievements)
               .WithOne(a => a.Doctor)
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Faqs)
               .WithOne(f => f.Doctor)
               .HasForeignKey(f => f.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.AppointmentOptions)
               .WithOne(a => a.Doctor)
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.SocialLinks)
               .WithOne(s => s.Doctor)
               .HasForeignKey(s => s.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Articles)
               .WithOne(a => a.Doctor)
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.Reviews)
               .WithOne(r => r.Doctor)
               .HasForeignKey(r => r.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.HeroImages)
               .WithOne(h => h.Doctor)
               .HasForeignKey(h => h.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
