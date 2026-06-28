namespace Accounting.Domain.Entities;

public class HeroImage
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public string KeyName { get; set; } = string.Empty;
    public string StorageUrl { get; set; } = string.Empty;
    public short SortOrder { get; set; }
    public bool IsActive { get; set; }

    /// <summary>Localised alt text stored as JSON string.</summary>
    public string AltText { get; set; } = string.Empty;

    // Navigation
    public Doctor? Doctor { get; set; }
}
