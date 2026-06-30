namespace Doctor.Application.DTOs.HeroImage;

public class CreateHeroImageDto
{
    public Guid DoctorId { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public Dictionary<string, string> Caption { get; set; } = new();
    public int SortOrder { get; set; }
}
