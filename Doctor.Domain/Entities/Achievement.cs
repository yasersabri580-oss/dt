namespace Doctor.Domain.Entities;

public class Achievement
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public short SortOrder { get; set; }
    public bool IsActive { get; set; }

    /// <summary>Localised achievement text stored as JSON string.</summary>
    public Dictionary<string, string> Text { get; set; } = new();

    // Navigation
    public Doctor? Doctor { get; set; }
}
