namespace Doctor.Application.DTOs.AppointmentOption;

public class UpdateAppointmentOptionDto
{
    public Dictionary<string, string>? Title { get; set; }
    public int? DurationMinutes { get; set; }
    public decimal? Price { get; set; }
    public bool? IsActive { get; set; }
}
