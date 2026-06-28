namespace Accounting.Domain.Entities;

public class Qualification
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }

    /// <summary>Year or year-range label, e.g. "2012" or "2010–2014".</summary>
    public string YearLabel { get; set; } = string.Empty;

    public short SortOrder { get; set; }
    public bool IsActive { get; set; }

    /// <summary>Localised title stored as JSON string.</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Localised body stored as JSON string.</summary>
    public string Body { get; set; } = string.Empty;

    // Navigation
    public Doctor? Doctor { get; set; }
}
