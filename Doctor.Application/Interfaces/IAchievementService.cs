using Doctor.Application.DTOs.Achievement;

namespace Doctor.Application.Interfaces;

public interface IAchievementService
{
    Task<List<AchievementDto>> GetAllAsync();
    Task<AchievementDto?> GetByIdAsync(long id);
    Task<AchievementDto?> GetByUserIdAsync(long userId);
    Task<AchievementDto?> GetByDoctorIdAsync(Guid doctorId);
    Task<AchievementDto> CreateAsync(CreateAchievementDto dto);
    Task UpdateAsync(long id, UpdateAchievementDto dto);
    Task DeleteAsync(long id);
}
