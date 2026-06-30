namespace Doctor.Application.DTOs.SocialLink;

public class SocialLinkDto
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
