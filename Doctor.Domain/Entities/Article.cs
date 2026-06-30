namespace Doctor.Domain.Entities;

public class Article
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public string Slug { get; set; } = string.Empty;
    public short ReadingMinutes { get; set; }
    public DateOnly PublishedAt { get; set; }

    /// <summary>URL of the cover image. Nullable.</summary>
    public string? CoverUrl { get; set; }

    public bool IsPublished { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; }

    // ── jsonb columns ───────────────────────────────────────────────────────
    /// <summary>Localised title stored as JSON map (e.g. {"en":"...", "fa":"..."}).</summary>
    public Dictionary<string, string> Title { get; set; } = new();

    /// <summary>Localised excerpt stored as JSON map (e.g. {"en":"...", "fa":"..."}).</summary>
    public Dictionary<string, string> Excerpt { get; set; } = new();

    /// <summary>Localised category stored as JSON map (e.g. {"en":"...", "fa":"..."}).</summary>
    public Dictionary<string, string> Category { get; set; } = new();

    /// <summary>Localised rich content stored as JSON map (e.g. {"en":"...", "fa":"..."}).</summary>
    public Dictionary<string, string> Content { get; set; } = new();

    // Navigation
    public Doctor? Doctor { get; set; }
}
