namespace Doctor.Domain.Interfaces;

/// <summary>User information returned by an external OAuth provider.</summary>
public record ExternalUserInfo(string ProviderUserId, string? Email, string? DisplayName);

/// <summary>
/// Validates a token with an external OAuth provider and returns
/// the user's identity information.
/// </summary>
public interface IExternalAuthProvider
{
    /// <summary>The canonical provider name used in <see cref="Entities.UserExternalLogin"/>.</summary>
    string ProviderName { get; }

    /// <summary>
    /// Validates the provider-specific token and returns user information.
    /// Throws <see cref="UnauthorizedAccessException"/> if the token is invalid.
    /// </summary>
    Task<ExternalUserInfo> ValidateTokenAsync(string token);
}
