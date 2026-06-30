namespace Doctor.Application.DTOs.Qualification;

public class UpdateQualificationDto
{
    public Dictionary<string, string>? Title { get; set; }
    public Dictionary<string, string>? Institution { get; set; }
    public int? Year { get; set; }
}
