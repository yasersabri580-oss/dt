namespace Doctor.Application.DTOs.Review;

public class ReviewDto
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
