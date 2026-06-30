namespace Doctor.Application.DTOs.Qualification;

public class QualificationDto
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public Dictionary<string, string> Title { get; set; } = new();
    public Dictionary<string, string> Institution { get; set; } = new();
    public int? Year { get; set; }
}
