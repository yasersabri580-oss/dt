using Doctor.Application.DTOs.HeroImage;

namespace Doctor.Application.Interfaces;

public interface IHeroImageService
{
    Task<IEnumerable<HeroImageDto>> GetAllAsync();
    Task<HeroImageDto?> GetByIdAsync(long id);
    Task<HeroImageDto?> GetByUserIdAsync(long userId);
    Task<HeroImageDto?> GetByDoctorIdAsync(Guid doctorId);
    Task<HeroImageDto> CreateAsync(CreateHeroImageDto dto);
    Task UpdateAsync(long id, UpdateHeroImageDto dto);
    Task DeleteAsync(long id);
}
