namespace Doctor.Application.DTOs.Doctor;

public class CreateDoctorDto
{
    /// <summary>The existing User account this doctor profile belongs to.</summary>
    public long UserId { get; set; }

    /// <summary>Localised slug map, e.g. {"en":"dr-john","fa":"دکتر-جان"}.</summary>
    public Dictionary<string, string> Slug { get; set; } = new();

    public bool IsActive { get; set; } = true;
}