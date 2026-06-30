namespace Doctor.Application.DTOs.ContactInfo;

public class ContactInfoDto
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public Dictionary<string, string> Address { get; set; } = new();
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? MapUrl { get; set; }
}
