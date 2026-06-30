namespace Doctor.Application.DTOs.Achievement;

public class UpdateAchievementDto
{
    public Dictionary<string, string>? Title { get; set; }
    public Dictionary<string, string>? Description { get; set; }
    public int? Year { get; set; }
    public string? IconUrl { get; set; }
}
