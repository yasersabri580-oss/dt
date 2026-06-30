namespace Doctor.Application.DTOs.AboutHighlight;

public class UpdateAboutHighlightDto
{
    public Dictionary<string, string>? Title { get; set; }
    public Dictionary<string, string>? Description { get; set; }
    public string? IconUrl { get; set; }
    public int? SortOrder { get; set; }
}
