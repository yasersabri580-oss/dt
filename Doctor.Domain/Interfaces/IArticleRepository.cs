// IArticleRepository.cs
using Doctor.Domain.Entities;

namespace Doctor.Domain.Interfaces;

public interface IArticleRepository
{
    Task<IEnumerable<Article>> GetAllAsync();
    Task<Article?> GetByIdAsync(long id);
    Task<Article?> GetByUserIdAsync(long userId);
    Task AddAsync(Article article);
    void Update(Article article);
    void Delete(Article article);
    Task<IEnumerable<Article>> GetArticlesByDoctorIdAsync(Guid doctorId);
}