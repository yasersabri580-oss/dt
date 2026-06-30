namespace Doctor.Application.DTOs.DoctorService;

public class DoctorServiceDto
{
    public long Id { get; set; }
    public Guid DoctorId { get; set; }
    public Dictionary<string, string> Title { get; set; } = new();
    public Dictionary<string, string> Description { get; set; } = new();
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
}
