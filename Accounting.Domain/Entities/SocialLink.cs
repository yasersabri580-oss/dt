namespace Accounting.Domain.Entities;

public class SocialLink
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public string Label { get; set; } = string.Empty;
    public string Href { get; set; } = string.Empty;

    /// <summary>Icon identifier. Nullable.</summary>
    public string? IconName { get; set; }

    public short SortOrder { get; set; }
    public bool IsActive { get; set; }

    // Navigation
    public Doctor? Doctor { get; set; }
}
