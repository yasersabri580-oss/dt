using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Repositories;
using Doctor.Infrastructure.Data;

namespace Doctor.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _db;

    public IUserRepository Users { get; }
    public IRefreshTokenRepository RefreshTokens { get; }
    public IUserExternalLoginRepository ExternalLogins { get; }
    public IDoctorRepository Doctors { get; }
    public IDoctorProfileRepository DoctorProfiles { get; }

    public UnitOfWork(AppDbContext db)
    {
        _db = db;

        Users = new UserRepository(db);
        RefreshTokens = new RefreshTokenRepository(db);
        ExternalLogins = new UserExternalLoginRepository(db);
        Doctors = new DoctorRepository(db);
        DoctorProfiles = new DoctorProfileRepository(db);
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default) =>
        _db.SaveChangesAsync(ct);

    public void Dispose() => _db.Dispose();
}