namespace Doctor.Application.DTOs.Stat;

public class CreateStatDto
{
    public Guid DoctorId { get; set; }
    public Dictionary<string, string> Label { get; set; } = new();
    public string Value { get; set; } = string.Empty;
    public string? IconUrl { get; set; }
}
