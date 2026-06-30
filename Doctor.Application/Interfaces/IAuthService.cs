using Doctor.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Doctor.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> LoginAsync(LoginDto dto, string? ipAddress = null, string? userAgent = null);
    Task<AuthResponseDto> RefreshAsync(string refreshToken, string? ipAddress = null, string? userAgent = null);
    Task LogoutAsync(string refreshToken);
    Task<AuthResponseDto> RegisterUserAsync(RegisterUserDto dto, string? ipAddress = null, string? userAgent = null);

    /// <summary>Authenticate using an external OAuth provider (Google, GitHub).</summary>
    Task<AuthResponseDto> LoginWithExternalProviderAsync(ExternalLoginDto dto, string? ipAddress, string? userAgent);

    /// <summary>List all active sessions for the given user.</summary>
    Task<List<SessionInfoDto>> GetSessionsAsync(long userId, Guid? currentSessionId = null);

    /// <summary>Revoke a specific session belonging to the user.</summary>
    Task RevokeSessionAsync(long userId, Guid sessionId);

    /// <summary>Revoke all sessions for the user except the current one.</summary>
    Task RevokeOtherSessionsAsync(long userId, Guid currentSessionId);

    /// <summary>Revoke all sessions for the user (sign out everywhere).</summary>
    Task RevokeAllSessionsAsync(long userId);
}
