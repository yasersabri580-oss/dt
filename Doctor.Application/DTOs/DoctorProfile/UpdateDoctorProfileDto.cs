namespace Doctor.Application.DTOs.DoctorProfile;

public class UpdateDoctorProfileDto
{
    public short? ExperienceYears { get; set; }
    public string? OgLocale { get; set; }

    public Dictionary<string, string>? FullName { get; set; }
    public Dictionary<string, string>? BrandSubline { get; set; }
    public Dictionary<string, string>? Tagline { get; set; }
    public Dictionary<string, string>? HeroTitle { get; set; }
    public Dictionary<string, string>? HeroCopy { get; set; }
    public Dictionary<string, string>? PrimaryCta { get; set; }
    public Dictionary<string, string>? SecondaryCta { get; set; }
    public Dictionary<string, string>? Mission { get; set; }
    public Dictionary<string, string>? AboutParagraph { get; set; }
    public Dictionary<string, string>? Schedule { get; set; }
    public Dictionary<string, string>? FooterCopy { get; set; }
    public Dictionary<string, string>? SeoTitle { get; set; }
    public Dictionary<string, string>? SeoTitleTemplate { get; set; }
    public Dictionary<string, string>? SeoDescription { get; set; }
    public Dictionary<string, string>? SeoKeywords { get; set; }
    public Dictionary<string, string>? OgTitle { get; set; }
    public Dictionary<string, string>? OgDescription { get; set; }
}