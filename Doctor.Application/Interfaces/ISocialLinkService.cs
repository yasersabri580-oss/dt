using Doctor.Application.DTOs.SocialLink;

namespace Doctor.Application.Interfaces;

public interface ISocialLinkService
{
    Task<IEnumerable<SocialLinkDto>> GetAllAsync();
    Task<SocialLinkDto?> GetByIdAsync(long id);
    Task<SocialLinkDto?> GetByUserIdAsync(long userId);
    Task<SocialLinkDto?> GetByDoctorIdAsync(Guid doctorId);
    Task<SocialLinkDto> CreateAsync(CreateSocialLinkDto dto);
    Task UpdateAsync(long id, UpdateSocialLinkDto dto);
    Task DeleteAsync(long id);
}
