namespace Doctor.Domain.Entities;

/// <summary>
/// Rich profile data for a doctor.
/// All jsonb columns are stored as JSON strings.
/// </summary>
public class DoctorProfile
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public short ExperienceYears { get; set; }

    /// <summary>Open-Graph locale, e.g. "en_US". Nullable.</summary>
    public string? OgLocale { get; set; }

    public DateTime UpdatedAt { get; set; }
    public DateTime? CreatedAt { get; set; }

    // ── jsonb columns (stored as JSON strings) ──────────────────────────────
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

    // Navigation
    public Doctor? Doctor { get; set; }
}
