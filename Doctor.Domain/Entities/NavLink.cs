namespace Doctor.Domain.Entities;

public class NavLink
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public string Href { get; set; } = string.Empty;
    public short SortOrder { get; set; }
    public bool IsActive { get; set; }

    /// <summary>Localised label stored as JSON string.</summary>
    public Dictionary<string, string> Label { get; set; } = new();

    // Navigation
    public Doctor? Doctor { get; set; }
}
