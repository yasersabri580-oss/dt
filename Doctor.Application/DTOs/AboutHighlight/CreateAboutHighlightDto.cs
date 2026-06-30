namespace Doctor.Application.DTOs.AboutHighlight;

public class CreateAboutHighlightDto
{
    public Guid DoctorId { get; set; }
    public Dictionary<string, string> Title { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
    public string? IconUrl { get; set; }
    public int SortOrder { get; set; }
}
