namespace Doctor.Application.DTOs.Auth;

public class LoginDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserType { get; set; } = "User";

    /// <summary>Optional human-readable device name (e.g. "iPhone 14").</summary>
    public string? DeviceName { get; set; }

    /// <summary>Optional client-generated unique device identifier.</summary>
    public string? DeviceId { get; set; }
}
