using Doctor.Application.DTOs.TechnologyHighlight;

namespace Doctor.Application.Interfaces;

public interface ITechnologyHighlightService
{
    Task<IEnumerable<TechnologyHighlightDto>> GetAllAsync();
    Task<TechnologyHighlightDto?> GetByIdAsync(long id);
    Task<TechnologyHighlightDto?> GetByUserIdAsync(long userId);
    Task<TechnologyHighlightDto?> GetByDoctorIdAsync(Guid doctorId);
    Task<TechnologyHighlightDto> CreateAsync(CreateTechnologyHighlightDto dto);
    Task UpdateAsync(long id, UpdateTechnologyHighlightDto dto);
    Task DeleteAsync(long id);
}
