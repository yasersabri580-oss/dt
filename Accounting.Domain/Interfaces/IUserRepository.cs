using Accounting.Domain.Entities;
namespace Accounting.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByUserNameAsync(string userName);
    /// <summary>Returns true if at least one user with Role = "Admin" exists.</summary>
    Task<bool> AdminExistsAsync();
    /// <summary>Returns true if any user exists at all (used for first-run bootstrap).</summary>
    Task<bool> AnyUserExistsAsync();
    Task AddAsync(User user);
    void Update(User user);
    void Delete(User user);
}
