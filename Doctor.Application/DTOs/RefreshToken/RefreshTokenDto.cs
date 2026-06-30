namespace Doctor.Application.DTOs.RefreshToken;

public class RefreshTokenDto
{
    public Guid SessionId { get; set; }
    public long UserId { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUsedAt { get; set; }
    public bool IsRevoked { get; set; }
}
