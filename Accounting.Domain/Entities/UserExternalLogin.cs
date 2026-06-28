namespace Accounting.Domain.Entities;

/// <summary>
/// Links a local User account to an external OAuth provider identity
/// (e.g. Google, GitHub).  One user may have multiple external logins.
/// </summary>
public class UserExternalLogin
{
    public long Id { get; set; }

    /// <summary>The local User this external identity belongs to.</summary>
    public long UserId { get; set; }

    /// <summary>Provider name, e.g. "Google" or "GitHub".</summary>
    public string Provider { get; set; } = string.Empty;

    /// <summary>The user's unique ID on the external provider.</summary>
    public string ProviderUserId { get; set; } = string.Empty;

    /// <summary>E-mail returned by the provider (may be null).</summary>
    public string? Email { get; set; }

    /// <summary>Display name returned by the provider (may be null).</summary>
    public string? DisplayName { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
