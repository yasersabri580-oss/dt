using System.Text.Json;
using Accounting.Domain.Interfaces;

namespace Accounting.Infrastructure.ExternalAuth;

/// <summary>
/// Validates a Google ID token by calling Google's tokeninfo endpoint.
/// The frontend obtains this token via the Google Sign-In / One Tap flow.
/// </summary>
public class GoogleAuthProvider : IExternalAuthProvider
{
    private readonly HttpClient _http;

    public GoogleAuthProvider(HttpClient http) => _http = http;

    public string ProviderName => "Google";

    public async Task<ExternalUserInfo> ValidateTokenAsync(string token)
    {
        var url = $"https://oauth2.googleapis.com/tokeninfo?id_token={Uri.EscapeDataString(token)}";
        using var response = await _http.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new UnauthorizedAccessException("Invalid Google ID token.");

        using var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        var root = doc.RootElement;

        var sub = root.TryGetProperty("sub", out var subEl) ? subEl.GetString() : null;
        if (string.IsNullOrEmpty(sub))
            throw new UnauthorizedAccessException("Google token did not contain a subject claim.");

        var email = root.TryGetProperty("email", out var emailEl) ? emailEl.GetString() : null;
        var name = root.TryGetProperty("name", out var nameEl) ? nameEl.GetString() : null;

        return new ExternalUserInfo(sub, email, name);
    }
}
