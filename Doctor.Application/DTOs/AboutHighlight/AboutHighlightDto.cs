namespace Doctor.Application.DTOs.AboutHighlight;

public class AboutHighlightDto
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public Dictionary<string, string> Title { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
    public string? IconUrl { get; set; }
    public int SortOrder { get; set; }
}
