namespace Doctor.Application.DTOs.HeroImage;

public class UpdateHeroImageDto
{
    public string? ImageUrl { get; set; }
    public Dictionary<string, string>? Caption { get; set; }
    public int? SortOrder { get; set; }
}
