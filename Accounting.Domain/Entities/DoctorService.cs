namespace Accounting.Domain.Entities;

/// <summary>A medical/clinical service offered by the doctor (maps to table "services").</summary>
public class DoctorService
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public string IconName { get; set; } = string.Empty;
    public short SortOrder { get; set; }
    public bool IsActive { get; set; }

    /// <summary>Localised title stored as JSON string.</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Localised body stored as JSON string.</summary>
    public string Body { get; set; } = string.Empty;

    // Navigation
    public Doctor? Doctor { get; set; }
}
