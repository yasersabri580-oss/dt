namespace Accounting.Domain.Entities;

public class RefreshToken
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>Unique session identifier for this device session.</summary>
    public Guid SessionId { get; set; } = Guid.NewGuid();

    /// <summary>Human-readable device name, e.g. "iPhone 14" or "Chrome on Windows".</summary>
    public string? DeviceName { get; set; }

    /// <summary>Client-generated unique device identifier (persisted on the client).</summary>
    public string? DeviceId { get; set; }

    /// <summary>IP address captured at login/refresh time.</summary>
    public string? IpAddress { get; set; }

    /// <summary>HTTP User-Agent string captured at login/refresh time.</summary>
    public string? UserAgent { get; set; }

    /// <summary>Timestamp of the last successful token refresh.</summary>
    public DateTime? LastUsedAt { get; set; }
}
