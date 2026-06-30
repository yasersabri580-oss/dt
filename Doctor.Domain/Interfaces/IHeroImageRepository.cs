
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IHeroImageRepository
{
    Task<IEnumerable<HeroImage>> GetAllAsync();
    Task<HeroImage> GetByIdAsync(Guid id);
    Task<HeroImage> GetByUserIdAsync(long userId);
    Task AddAsync(HeroImage heroImage);
    void Update(HeroImage heroImage);
    void Delete(HeroImage heroImage);
}