using Accounting.Domain.Entities;

namespace Accounting.Domain.Interfaces;

public interface IUserExternalLoginRepository
{
    Task<UserExternalLogin?> GetAsync(string provider, string providerUserId);
    Task<List<UserExternalLogin>> GetByUserIdAsync(long userId);
    Task AddAsync(UserExternalLogin login);
}
