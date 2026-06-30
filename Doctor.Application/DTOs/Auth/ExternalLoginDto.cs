namespace Doctor.Application.DTOs.Auth;

/// <summary>Request payload for logging in via an external OAuth provider.</summary>
public class ExternalLoginDto
{
    /// <summary>Provider name: "Google" or "GitHub".</summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>
    /// The token issued by the external provider.
    /// For Google: an ID token (JWT).
    /// For GitHub: an access token.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>Optional human-readable device name (e.g. "iPhone 14").</summary>
    public string? DeviceName { get; set; }

    /// <summary>Optional client-generated unique device identifier.</summary>
    public string? DeviceId { get; set; }
}
