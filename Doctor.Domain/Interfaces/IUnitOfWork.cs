namespace Doctor.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IUserExternalLoginRepository ExternalLogins { get; }
    IRefreshTokenRepository RefreshTokens { get; }
    IDoctorRepository Doctors { get; }
    IDoctorProfileRepository DoctorProfiles { get; }

    Task<int> SaveChangesAsync(CancellationToken ct = default);
}