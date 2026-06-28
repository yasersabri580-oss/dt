namespace Accounting.Domain.Entities;

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
    public string FullName { get; set; } = string.Empty;
    public string BrandSubline { get; set; } = string.Empty;
    public string Tagline { get; set; } = string.Empty;
    public string HeroTitle { get; set; } = string.Empty;
    public string HeroCopy { get; set; } = string.Empty;
    public string PrimaryCta { get; set; } = string.Empty;
    public string SecondaryCta { get; set; } = string.Empty;
    public string Mission { get; set; } = string.Empty;
    public string AboutParagraph { get; set; } = string.Empty;
    public string Schedule { get; set; } = string.Empty;
    public string FooterCopy { get; set; } = string.Empty;
    public string SeoTitle { get; set; } = string.Empty;
    public string SeoTitleTemplate { get; set; } = string.Empty;
    public string SeoDescription { get; set; } = string.Empty;
    public string SeoKeywords { get; set; } = string.Empty;
    public string OgTitle { get; set; } = string.Empty;
    public string OgDescription { get; set; } = string.Empty;

    // Navigation
    public Doctor? Doctor { get; set; }
}
