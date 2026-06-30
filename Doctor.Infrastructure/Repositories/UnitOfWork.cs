using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;

namespace Doctor.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;

    public IUserRepository Users { get; }
    public IUserExternalLoginRepository ExternalLogins { get; }
    public IRefreshTokenRepository RefreshTokens { get; }
    public IDoctorRepository Doctors { get; }
    public IDoctorProfileRepository DoctorProfiles { get; }
    public IAboutHighlightRepository AboutHighlights { get; }
    public IAchivementRepository Achievements { get; }
    public IAppointmentOptionRepository AppointmentOptions { get; }
    public IArticleRepository Articles { get; }
    public IContactInfoRepository ContactInfos { get; }
    public IDoctorServiceRepository DoctorServices { get; }
    public IFaqRepository Faqs { get; }
    public IHeroImageRepository HeroImages { get; }
    public IQualificationRepository Qualifications { get; }
    public IReviewRepository Reviews { get; }
    public ISocialLinkRepository SocialLinks { get; }
    public IStatRepository Stats { get; }
    public ITechnologyHighlightRepository TechnologyHighlights { get; }

    public UnitOfWork(AppDbContext db)
    {
        _db = db;

        Users = new UserRepository(db);
        RefreshTokens = new RefreshTokenRepository(db);
        ExternalLogins = new UserExternalLoginRepository(db);
        Doctors = new DoctorRepository(db);
        DoctorProfiles = new DoctorProfileRepository(db);
        AboutHighlights = new AboutHighlightRepository(db);
        Achievements = new AchievementRepository(db);
        AppointmentOptions = new AppointmentOptionRepository(db);
        Articles = new ArticleRepository(db);
        ContactInfos = new ContactInfoRepository(db);
        DoctorServices = new DoctorServiceRepository(db);
        Faqs = new FaqRepository(db);
        HeroImages = new HeroImageRepository(db);
        Qualifications = new QualificationRepository(db);
        Reviews = new ReviewRepository(db);
        SocialLinks = new SocialLinkRepository(db);
        Stats = new StatRepository(db);
        TechnologyHighlights = new TechnologyHighlightRepository(db);
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        _db.SaveChangesAsync(ct);

    public void Dispose() => _db.Dispose();
}