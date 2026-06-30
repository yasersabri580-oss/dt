namespace Doctor.Application.DTOs.Faq;

public class UpdateFaqDto
{
    public Dictionary<string, string>? Question { get; set; }
    public Dictionary<string, string>? Answer { get; set; }
    public int? SortOrder { get; set; }
}
