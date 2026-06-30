namespace Doctor.Application.DTOs.DoctorService;

public class UpdateDoctorServiceDto
{
    public Dictionary<string, string>? Title { get; set; }
    public Dictionary<string, string>? Description { get; set; }
    public decimal? Price { get; set; }
    public bool? IsActive { get; set; }
}
