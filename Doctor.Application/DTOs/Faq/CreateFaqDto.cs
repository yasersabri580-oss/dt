namespace Doctor.Application.DTOs.Faq;

public class CreateFaqDto
{
    public Guid DoctorId { get; set; }
    public Dictionary<string, string> Question { get; set; } = new();
    public Dictionary<string, string> Answer { get; set; } = new();
    public int SortOrder { get; set; }
}
