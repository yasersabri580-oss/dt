using System.Security.Claims;
using Accounting.Application.DTOs.Auth;
using Accounting.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounting_helal.Controllers;

/// <summary>
/// Handles user authentication: registration, login, token refresh, logout,
/// external OAuth provider login, and device session management.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _auth;
    public AuthController(IAuthService auth) => _auth = auth;

    // ── Standard username/password ───────────────────────────────────────────

    /// <summary>Register a new user account.</summary>
    /// <remarks>
    /// Creates a new user with the given credentials and returns a JWT token pair.
    /// The default role is "User" unless specified otherwise.
    ///
    /// **Sample request:**
    /// ```json
    /// {
    ///   "userName": "john_doe",
    ///   "password": "Secret@123",
    ///   "nameK": "John Doe",
    ///   "codeM": "C001",
    ///   "role": "User"
    /// }
    /// ```
    /// </remarks>
    /// <param name="dto">Registration details.</param>
    /// <response code="200">Registration successful; returns access and refresh tokens.</response>
    /// <response code="409">Username already exists.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        try
        {
            var result = await _auth.RegisterUserAsync(dto, GetIp(), GetUserAgent());
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    /// <summary>Authenticate and receive a JWT token pair.</summary>
    /// <remarks>
    /// **Sample request:**
    /// ```json
    /// {
    ///   "userName": "john_doe",
    ///   "password": "Secret@123",
    ///   "userType": "User",
    ///   "deviceName": "Chrome on Windows",
    ///   "deviceId": "optional-client-device-id"
    /// }
    /// ```
    /// `userType` accepts: `"User"` or `"Karbar"`.
    /// </remarks>
    /// <param name="dto">Login credentials.</param>
    /// <response code="200">Login successful; returns access and refresh tokens.</response>
    /// <response code="401">Invalid credentials.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        try
        {
            var result = await _auth.LoginAsync(dto, GetIp(), GetUserAgent());
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>Refresh an expired access token using a valid refresh token.</summary>
    /// <param name="dto">The refresh token.</param>
    /// <response code="200">New access and refresh token pair.</response>
    /// <response code="401">Refresh token is invalid or expired.</response>
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto dto)
    {
        try
        {
            var result = await _auth.RefreshAsync(dto.RefreshToken, GetIp(), GetUserAgent());
            return Ok(result);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>Revoke a refresh token (logout from current device).</summary>
    /// <param name="dto">The refresh token to revoke.</param>
    /// <response code="204">Token revoked successfully.</response>
    [HttpPost("logout")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto dto)
    {
        await _auth.LogoutAsync(dto.RefreshToken);
        return NoContent();
    }

    // ── External OAuth providers ─────────────────────────────────────────────

    /// <summary>Login or auto-register via an external OAuth provider (Google, GitHub).</summary>
    /// <remarks>
    /// The frontend completes the OAuth flow and sends the resulting token here.
    ///
    /// **Sample request (Google):**
    /// ```json
    /// {
    ///   "provider": "Google",
    ///   "token": "&lt;google-id-token&gt;",
    ///   "deviceName": "iPhone 14"
    /// }
    /// ```
    /// **Sample request (GitHub):**
    /// ```json
    /// {
    ///   "provider": "GitHub",
    ///   "token": "&lt;github-access-token&gt;"
    /// }
    /// ```
    /// If the provider identity is not yet linked to any local account, a new account
    /// is created automatically using the e-mail (or provider ID) as the username.
    /// </remarks>
    /// <param name="dto">Provider name and token.</param>
    /// <response code="200">Authentication successful.</response>
    /// <response code="400">Unsupported provider.</response>
    /// <response code="401">Token rejected by the provider.</response>
    [HttpPost("external-login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ExternalLogin([FromBody] ExternalLoginDto dto)
    {
        try
        {
            var result = await _auth.LoginWithExternalProviderAsync(dto, GetIp(), GetUserAgent());
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    // ── Device session management ────────────────────────────────────────────

    /// <summary>List all active sessions for the authenticated user.</summary>
    /// <remarks>
    /// Each session represents a device that holds a valid refresh token.
    /// The `isCurrent` flag identifies the session that matches the `currentSessionId` query parameter.
    /// </remarks>
    /// <param name="currentSessionId">
    /// Optional. The <c>sessionId</c> returned when you logged in; used to mark which
    /// session is the caller's own session.
    /// </param>
    /// <response code="200">List of active sessions.</response>
    /// <response code="401">Not authenticated.</response>
    [Authorize]
    [HttpGet("sessions")]
    [ProducesResponseType(typeof(List<SessionInfoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetSessions([FromQuery] Guid? currentSessionId = null)
    {
        var userId = GetUserId();
        var sessions = await _auth.GetSessionsAsync(userId, currentSessionId);
        return Ok(sessions);
    }

    /// <summary>Revoke a specific session (sign out a device).</summary>
    /// <param name="sessionId">The session ID to revoke.</param>
    /// <response code="204">Session revoked.</response>
    /// <response code="400">Session not found or does not belong to the user.</response>
    /// <response code="401">Not authenticated.</response>
    [Authorize]
    [HttpDelete("sessions/{sessionId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RevokeSession(Guid sessionId)
    {
        try
        {
            await _auth.RevokeSessionAsync(GetUserId(), sessionId);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>Revoke all other sessions (sign out every other device).</summary>
    /// <param name="currentSessionId">The caller's current session ID (will NOT be revoked).</param>
    /// <response code="204">Other sessions revoked.</response>
    /// <response code="401">Not authenticated.</response>
    [Authorize]
    [HttpDelete("sessions/others")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RevokeOtherSessions([FromQuery] Guid currentSessionId)
    {
        await _auth.RevokeOtherSessionsAsync(GetUserId(), currentSessionId);
        return NoContent();
    }

    /// <summary>Revoke all sessions (sign out from all devices).</summary>
    /// <response code="204">All sessions revoked.</response>
    /// <response code="401">Not authenticated.</response>
    [Authorize]
    [HttpDelete("sessions")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RevokeAllSessions()
    {
        await _auth.RevokeAllSessionsAsync(GetUserId());
        return NoContent();
    }

    // ── Helpers ──────────────────────────────────────────────────────────────

    private long GetUserId()
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? User.FindFirstValue("sub")
            ?? throw new UnauthorizedAccessException("User ID not found in token.");
        return long.Parse(sub);
    }

    private string? GetIp() =>
        HttpContext.Connection.RemoteIpAddress?.ToString();

    private string? GetUserAgent() =>
        Request.Headers.UserAgent.ToString() is { Length: > 0 } ua ? ua : null;
}
