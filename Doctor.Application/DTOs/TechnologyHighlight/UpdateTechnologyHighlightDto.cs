namespace Doctor.Application.DTOs.TechnologyHighlight;

public class UpdateTechnologyHighlightDto
{
    public Dictionary<string, string>? Title { get; set; }
    public Dictionary<string, string>? Description { get; set; }
    public string? IconUrl { get; set; }
}
