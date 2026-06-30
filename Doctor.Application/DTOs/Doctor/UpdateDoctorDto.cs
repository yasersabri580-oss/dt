namespace Doctor.Application.DTOs.Doctor;

public class UpdateDoctorDto
{
    public Dictionary<string, string>? Slug { get; set; }
    public bool? IsActive { get; set; }
}