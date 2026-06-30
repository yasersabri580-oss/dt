namespace Doctor.Application.DTOs.Review;

public class CreateReviewDto
{
    public Guid DoctorId { get; set; }
    public string ReviewerName { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string? Comment { get; set; }
}
