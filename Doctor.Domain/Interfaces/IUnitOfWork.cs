namespace Doctor.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IUserExternalLoginRepository ExternalLogins { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IDoctorRepository Doctors { get; }
    IDoctorProfileRepository DoctorProfiles { get; }
    IAboutHighlightRepository AboutHighlights { get; }
    IAchivementRepository Achievements { get; }
    IAppointmentOptionRepository AppointmentOptions { get; }
    IArticleRepository Articles { get; }
    IContactInfoRepository ContactInfos { get; }
    IDoctorServiceRepository DoctorServices { get; }
    IFaqRepository Faqs { get; }
    IHeroImageRepository HeroImages { get; }
    IQualificationRepository Qualifications { get; }
    IReviewRepository Reviews { get; }
    ISocialLinkRepository SocialLinks { get; }
    IStatRepository Stats { get; }
    ITechnologyHighlightRepository TechnologyHighlights { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}