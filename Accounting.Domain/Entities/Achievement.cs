namespace Accounting.Domain.Entities;

public class Achievement
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public short SortOrder { get; set; }
    public bool IsActive { get; set; }

    /// <summary>Localised achievement text stored as JSON string.</summary>
    public string Text { get; set; } = string.Empty;

    // Navigation
    public Doctor? Doctor { get; set; }
}
