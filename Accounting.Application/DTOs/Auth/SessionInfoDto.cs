namespace Accounting.Application.DTOs.Auth;

/// <summary>Information about a single authenticated device session.</summary>
public class SessionInfoDto
{
    public Guid SessionId { get; set; }
    public string? DeviceName { get; set; }
    public string? DeviceId { get; set; }
    public string? IpAddress { get; set; }
    public string? UserAgent { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUsedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsCurrent { get; set; }
}
