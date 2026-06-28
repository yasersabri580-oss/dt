namespace Accounting.Domain.Entities;

public class User
{
    public long IdUser { get; set; }
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
    public string? NameK { get; set; }
    public string? CodeM { get; set; }
    public string? Role { get; set; } = "User";


    


}
