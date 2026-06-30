using Doctor.Application.DTOs.AboutHighlight;

namespace Doctor.Application.Interfaces;

public interface IAboutHighlightService
{
    Task<IEnumerable<AboutHighlightDto>> GetAllAsync();
    Task<AboutHighlightDto?> GetByIdAsync(long id);
    Task<AboutHighlightDto?> GetByUserIdAsync(long userId);
    Task<AboutHighlightDto?> GetByDoctorIdAsync(Guid doctorId);
    Task<AboutHighlightDto> CreateAsync(CreateAboutHighlightDto dto);
    Task UpdateAsync(long id, UpdateAboutHighlightDto dto);
    Task DeleteAsync(long id);
}
