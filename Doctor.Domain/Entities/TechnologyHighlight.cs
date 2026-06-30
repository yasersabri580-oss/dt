namespace Doctor.Domain.Entities;

public class TechnologyHighlight
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public short SortOrder { get; set; }
    public bool IsActive { get; set; }

    /// <summary>Localised title stored as JSON map (e.g. {"en":"...", "fa":"..."}).</summary>
    public Dictionary<string, string> Title { get; set; } = new();

    /// <summary>Localised body stored as JSON string.</summary>
    public Dictionary<string, string> Body { get; set; } = new();

    // Navigation
    public Doctor? Doctor { get; set; }
}
