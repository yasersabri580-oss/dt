namespace Accounting.Domain.Entities;

public class Review
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string Quote { get; set; } = string.Empty;
    public short Rating { get; set; }
    public bool IsActive { get; set; }
    public short SortOrder { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Doctor? Doctor { get; set; }
}
