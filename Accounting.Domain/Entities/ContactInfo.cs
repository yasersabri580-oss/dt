namespace Accounting.Domain.Entities;

public class ContactInfo
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public string PhoneDisplay { get; set; } = string.Empty;
    public string PhoneLink { get; set; } = string.Empty;
    public string WhatsappUrl { get; set; } = string.Empty;
    public DateTime UpdatedAt { get; set; }

    /// <summary>Localised address stored as JSON string.</summary>
    public string Address { get; set; } = string.Empty;

    // Navigation
    public Doctor? Doctor { get; set; }
}
