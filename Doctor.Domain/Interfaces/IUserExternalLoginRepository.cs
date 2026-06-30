using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IUserExternalLoginRepository
{
    Task<UserExternalLogin?> GetAsync(string provider, string providerUserId);
    Task<List<UserExternalLogin>> GetByUserIdAsync(long userId);
    Task AddAsync(UserExternalLogin login);
}
