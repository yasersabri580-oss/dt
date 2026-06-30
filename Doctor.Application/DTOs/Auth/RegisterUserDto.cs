namespace Doctor.Application.DTOs.Auth;

public class RegisterUserDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? NameK { get; set; }
    public string? CodeM { get; set; }
}
