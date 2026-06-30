using Doctor.Application.DTOs.Article;

namespace Doctor.Application.Interfaces;

public interface IArticleService
{
    Task<IEnumerable<ArticleDto>> GetAllAsync();
    Task<ArticleDto?> GetByIdAsync(long id);
    Task<ArticleDto?> GetByUserIdAsync(long userId);
    Task<IEnumerable<ArticleDto>> GetArticlesByDoctorIdAsync(Guid doctorId);
    Task<ArticleDto> CreateAsync(CreateArticleDto dto);
    Task UpdateAsync(long id, UpdateArticleDto dto);
    Task DeleteAsync(long id);
}
