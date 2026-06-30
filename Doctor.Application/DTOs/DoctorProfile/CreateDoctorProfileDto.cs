namespace Doctor.Application.DTOs.DoctorProfile;

public class CreateDoctorProfileDto
{
    public Guid DoctorId { get; set; }
    public short ExperienceYears { get; set; }
    public string? OgLocale { get; set; }

    public Dictionary<string, string> FullName { get; set; } = new();
    public Dictionary<string, string> BrandSubline { get; set; } = new();
    public Dictionary<string, string> Tagline { get; set; } = new();
    public Dictionary<string, string> HeroTitle { get; set; } = new();
    public Dictionary<string, string> HeroCopy { get; set; } = new();
    public Dictionary<string, string> PrimaryCta { get; set; } = new();
    public Dictionary<string, string> SecondaryCta { get; set; } = new();
    public Dictionary<string, string> Mission { get; set; } = new();
    public Dictionary<string, string> AboutParagraph { get; set; } = new();
    public Dictionary<string, string> Schedule { get; set; } = new();
    public Dictionary<string, string> FooterCopy { get; set; } = new();
    public Dictionary<string, string> SeoTitle { get; set; } = new();
    public Dictionary<string, string> SeoTitleTemplate { get; set; } = new();
    public Dictionary<string, string> SeoDescription { get; set; } = new();
    public Dictionary<string, string> SeoKeywords { get; set; } = new();
    public Dictionary<string, string> OgTitle { get; set; } = new();
    public Dictionary<string, string> OgDescription { get; set; } = new();
}