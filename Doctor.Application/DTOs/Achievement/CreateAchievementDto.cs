namespace Doctor.Application.DTOs.Achievement;

public class CreateAchievementDto
{
    public Guid DoctorId { get; set; }
    public Dictionary<string, string> Title { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
    public int? Year { get; set; }
    public string? IconUrl { get; set; }
}
