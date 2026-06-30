namespace Doctor.Domain.Entities;

public class Stat
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public int Value { get; set; }

    /// <summary>Optional unit suffix, e.g. "+", "%". Nullable.</summary>
    public string? Suffix { get; set; }

    public short SortOrder { get; set; }
    public bool IsActive { get; set; }

    /// <summary>Localised label stored as JSON string.</summary>
    public Dictionary<string, string> Label { get; set; } = new();

    /// <summary>Localised footnote/note stored as JSON string.</summary>
    public Dictionary<string, string> Note { get; set; } = new();

    // Navigation
    public Doctor? Doctor { get; set; }
}
