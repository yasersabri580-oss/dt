namespace Doctor.Domain.Entities;

public class Faq
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public short SortOrder { get; set; }
    public bool IsActive { get; set; }

    /// <summary>Localised question stored as JSON string.</summary>
    public Dictionary<string, string> Question { get; set; } = new();

    /// <summary>Localised answer stored as JSON string.</summary>
    public Dictionary<string, string> Answer { get; set; } = new();

    // Navigation
    public Doctor? Doctor { get; set; }
}
