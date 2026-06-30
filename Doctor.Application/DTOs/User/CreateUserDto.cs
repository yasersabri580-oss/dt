namespace Doctor.Application.DTOs.User;

public class CreateUserDto
{
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string? NameK { get; set; }
    public string? CodeM { get; set; }
    public string Role { get; set; } = "User";
}
