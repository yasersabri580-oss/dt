namespace Doctor.Application.DTOs.ContactInfo;

public class UpdateContactInfoDto
{
    public Dictionary<string, string>? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? MapUrl { get; set; }
}
