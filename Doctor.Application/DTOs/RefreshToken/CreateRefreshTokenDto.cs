namespace Doctor.Application.DTOs.RefreshToken;

/// <summary>Internal payload used when issuing a new refresh token; not exposed via API input.</summary>
public class CreateRefreshTokenDto
{
    public long UserId { get; set; }
    public Guid SessionId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
