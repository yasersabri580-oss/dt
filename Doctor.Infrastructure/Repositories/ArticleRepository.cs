// ArticleRepository.cs
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;
using Doctor.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Doctor.Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _db;
    public ArticleRepository(AppDbContext db) => _db = db;

    public async Task<IEnumerable<Article>> GetAllAsync() =>
        await _db.Articles.AsNoTracking().ToListAsync();

    public async Task<Article?> GetByIdAsync(long id) =>
        await _db.Articles.FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Article?> GetByUserIdAsync(long userId) =>
        await _db.Articles
            .AsNoTracking()
            .Join(_db.Doctors, a => a.DoctorId, d => d.Id, (a, d) => new { a, d.UserId })
            .Where(x => x.UserId == userId)
            .Select(x => x.a)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<Article>> GetArticlesByDoctorIdAsync(Guid doctorId) =>
        await _db.Articles.AsNoTracking().Where(a => a.DoctorId == doctorId).ToListAsync();

    public async Task AddAsync(Article article) =>
        await _db.Articles.AddAsync(article);

    public void Update(Article article) =>
        _db.Articles.Update(article);

    public void Delete(Article article) =>
        _db.Articles.Remove(article);
}