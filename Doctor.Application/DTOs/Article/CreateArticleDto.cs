namespace Doctor.Application.DTOs.Article;

public class CreateArticleDto
{
    public Guid DoctorId { get; set; }
    public Dictionary<string, string> Title { get; set; } = new();
    public Dictionary<string, string> Content { get; set; } = new();
    public Dictionary<string, string> Slug { get; set; } = new();
    public bool IsPublished { get; set; } = false;
}
