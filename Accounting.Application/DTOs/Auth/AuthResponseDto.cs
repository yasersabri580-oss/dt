

namespace Accounting.Application.DTOs.Auth;

public class AuthResponseDto
{
    public long UserId { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }

    /// <summary>Session identifier for this device session (use to revoke a specific device).</summary>
    public Guid SessionId { get; set; }

}
