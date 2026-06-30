using Doctor.Application.DTOs.DoctorProfile;

namespace Doctor.Application.DTOs.Doctor;

/// <summary>Combined doctor + profile payload returned by "get my profile" style endpoints.</summary>
public class DoctorWithProfileDto
{
    public Guid Id { get; set; }
    public long UserId { get; set; }
    public Dictionary<string, string> Slug { get; set; } = new();
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DoctorProfileDto? Profile { get; set; }
}