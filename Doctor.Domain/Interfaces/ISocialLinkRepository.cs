// ISocialLinkRepository.cs
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface ISocialLinkRepository
{
    Task<IEnumerable<SocialLink>> GetAllAsync();
    Task<SocialLink?> GetByIdAsync(long id);
    Task<SocialLink?> GetByUserIdAsync(long userId);
    Task AddAsync(SocialLink socialLink);
    void Update(SocialLink socialLink);
    void Delete(SocialLink socialLink);
    Task<SocialLink?> GetByDoctorIdAsync(Guid id);
}