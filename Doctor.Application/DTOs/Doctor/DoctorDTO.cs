namespace Doctor.Application.DTOs.Doctor;

public class DoctorDto
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public Dictionary<string, string> Slug { get; set; } = new();
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}