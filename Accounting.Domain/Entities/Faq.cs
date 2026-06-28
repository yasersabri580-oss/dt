namespace Accounting.Domain.Entities;

public class Faq
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public short SortOrder { get; set; }
    public bool IsActive { get; set; }

    /// <summary>Localised question stored as JSON string.</summary>
    public string Question { get; set; } = string.Empty;

    /// <summary>Localised answer stored as JSON string.</summary>
    public string Answer { get; set; } = string.Empty;

    // Navigation
    public Doctor? Doctor { get; set; }
}
