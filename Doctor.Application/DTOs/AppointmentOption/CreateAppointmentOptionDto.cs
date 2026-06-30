namespace Doctor.Application.DTOs.AppointmentOption;

public class CreateAppointmentOptionDto
{
    public Guid DoctorId { get; set; }
    public Dictionary<string, string> Title { get; set; } = new();
    public int DurationMinutes { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
}
