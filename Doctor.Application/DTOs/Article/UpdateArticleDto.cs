namespace Doctor.Application.DTOs.Article;

public class UpdateArticleDto
{
    public Dictionary<string, string>? Title { get; set; }
    public Dictionary<string, string>? Content { get; set; }
    public Dictionary<string, string>? Slug { get; set; }
    public bool? IsPublished { get; set; }
}
