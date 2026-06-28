namespace Accounting.Domain.Entities;

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
    /// <summary>Localised title stored as JSON string.</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Localised excerpt stored as JSON string.</summary>
    public string Excerpt { get; set; } = string.Empty;

    /// <summary>Localised category stored as JSON string.</summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>Localised rich content stored as JSON string.</summary>
    public string Content { get; set; } = string.Empty;

    // Navigation
    public Doctor? Doctor { get; set; }
}
