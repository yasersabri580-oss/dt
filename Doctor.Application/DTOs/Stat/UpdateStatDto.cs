namespace Doctor.Application.DTOs.Stat;

public class UpdateStatDto
{
    public Dictionary<string, string>? Label { get; set; }
    public string? Value { get; set; }
    public string? IconUrl { get; set; }
}
