
using Doctor.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // ── Existing sets ──────────────────────────────────────────────────────
    public DbSet<User> Users => Set<User>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<UserExternalLogin> UserExternalLogins => Set<UserExternalLogin>();

    // ── Doctor / profile sets ──────────────────────────────────────────────
    public DbSet<Doctor.Domain.Entities.Doctor> Doctors => Set<Doctor.Domain.Entities.Doctor>();
    public DbSet<DoctorProfile> DoctorProfiles => Set<DoctorProfile>();
    public DbSet<NavLink> NavLinks => Set<NavLink>();
    public DbSet<AboutHighlight> AboutHighlights => Set<AboutHighlight>();
    public DbSet<DoctorService> DoctorServices => Set<DoctorService>();
    public DbSet<TechnologyHighlight> TechnologyHighlights => Set<TechnologyHighlight>();
    public DbSet<Stat> Stats => Set<Stat>();
    public DbSet<Qualification> Qualifications => Set<Qualification>();
    public DbSet<Achievement> Achievements => Set<Achievement>();
    public DbSet<Faq> Faqs => Set<Faq>();
    public DbSet<AppointmentOption> AppointmentOptions => Set<AppointmentOption>();
    public DbSet<ContactInfo> ContactInfos => Set<ContactInfo>();
    public DbSet<SocialLink> SocialLinks => Set<SocialLink>();
    public DbSet<Article> Articles => Set<Article>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<HeroImage> HeroImages => Set<HeroImage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
