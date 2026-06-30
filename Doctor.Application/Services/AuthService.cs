
using Doctor.Application.DTOs.Auth;
using Doctor.Application.Interfaces;
using Doctor.Domain.Entities;
using Doctor.Domain.Interfaces;

namespace Doctor.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _uow;
    private readonly ITokenService _tokenService;
    private readonly IEnumerable<IExternalAuthProvider> _externalProviders;

    public AuthService(
        IUnitOfWork uow,
        ITokenService tokenService,
        IEnumerable<IExternalAuthProvider> externalProviders)


    {
        _uow = uow;
        _tokenService = tokenService;
        _externalProviders = externalProviders;
    }

    public async Task<AuthResponseDto> RegisterUserAsync(RegisterUserDto dto, string? ipAddress = null, string? userAgent = null)
    {
        var existing = await _uow.Users.GetByUserNameAsync(dto.UserName);
        if (existing != null)
            throw new InvalidOperationException("Username already exists.");

        // The very first registration ever becomes the Admin (bootstrap).
        // All subsequent registrations are always "User" — role cannot be self-assigned.
        var isFirstUser = !await _uow.Users.AnyUserExistsAsync();
        var role = isFirstUser ? "Admin" : "User";

        var user = new User
        {
            UserName = dto.UserName,
            NameK = dto.NameK,
            CodeM = dto.CodeM,
            Role = role,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };
        await _uow.Users.AddAsync(user);
        await _uow.SaveChangesAsync();

        return await CreateTokensForUserAsync(
            user.IdUser, user.UserName!, user.Role ?? "User",
            deviceName: null, deviceId: null, ipAddress: ipAddress, userAgent: userAgent);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto, string? ipAddress = null, string? userAgent = null)
    {
       
            var user = await _uow.Users.GetByUserNameAsync(dto.UserName)
                ?? throw new UnauthorizedAccessException("Invalid credentials.");

            if (string.IsNullOrEmpty(user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");

           
            return await CreateTokensForUserAsync(
                
                user.IdUser, user.UserName!, user.Role ?? "User",
                dto.DeviceName, dto.DeviceId, ipAddress, userAgent);
        
    }

    public async Task<AuthResponseDto> RefreshAsync(string refreshToken, string? ipAddress = null, string? userAgent = null)
    {
        var stored = await _uow.RefreshTokens.GetByTokenAsync(refreshToken)
            ?? throw new UnauthorizedAccessException("Invalid refresh token.");

        if (stored.IsRevoked || stored.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token expired or revoked.");

        // Preserve device info; rotate to a new token in the same session slot.
        var deviceName = stored.DeviceName;
        var deviceId = stored.DeviceId;
        stored.IsRevoked = true;
        await _uow.SaveChangesAsync();

        var user = await _uow.Users.GetByIdAsync(stored.UserId);
        if (user != null)
            return await CreateTokensForUserAsync(
                user.IdUser, user.UserName!, user.Role ?? "User",
                deviceName, deviceId, ipAddress ?? stored.IpAddress, userAgent ?? stored.UserAgent);

      

        throw new UnauthorizedAccessException("User not found.");
    }

    public async Task LogoutAsync(string refreshToken)
    {
        var stored = await _uow.RefreshTokens.GetByTokenAsync(refreshToken);
        if (stored != null)
        {
            stored.IsRevoked = true;
            await _uow.SaveChangesAsync();
        }
    }

    public async Task<AuthResponseDto> LoginWithExternalProviderAsync(
        ExternalLoginDto dto, string? ipAddress, string? userAgent)
    {
        var providerName = dto.Provider.Trim();
        var provider = _externalProviders.FirstOrDefault(
            p => p.ProviderName.Equals(providerName, StringComparison.OrdinalIgnoreCase))
            ?? throw new InvalidOperationException($"Unsupported provider: {providerName}");

        var info = await provider.ValidateTokenAsync(dto.Token);

        // Try to find an existing external login link.
        var extLogin = await _uow.ExternalLogins.GetAsync(providerName, info.ProviderUserId);

        long userId;
        string userName;
        string role;

        if (extLogin != null)
        {
            // Known external identity → look up the linked user.
            var linkedUser = await _uow.Users.GetByIdAsync(extLogin.UserId)
                ?? throw new UnauthorizedAccessException("Linked user not found.");
            userId = linkedUser.IdUser;
            userName = linkedUser.UserName!;
            role = linkedUser.Role ?? "User";
        }
        else
        {
            // New external identity → find or auto-create a local user.
            User? user = null;

            if (!string.IsNullOrEmpty(info.Email))
                user = await _uow.Users.GetByUserNameAsync(info.Email);

            if (user == null)
            {
                // Auto-create a new user account.
                var generatedUserName = info.Email
                    ?? $"{providerName.ToLower()}_{info.ProviderUserId}";

                user = new User
                {
                    UserName = generatedUserName,
                    NameK = info.DisplayName,
                    Role = "User"
                };
                await _uow.Users.AddAsync(user);
                await _uow.SaveChangesAsync();
            }

            // Create the external login link.
            await _uow.ExternalLogins.AddAsync(new UserExternalLogin
            {
                UserId = user.IdUser,
                Provider = providerName,
                ProviderUserId = info.ProviderUserId,
                Email = info.Email,
                DisplayName = info.DisplayName
            });
            await _uow.SaveChangesAsync();

            userId = user.IdUser;
            userName = user.UserName!;
            role = user.Role ?? "User";
        }

        return await CreateTokensForUserAsync(
            userId, userName, role,
            dto.DeviceName, dto.DeviceId, ipAddress, userAgent);
    }

    public async Task<List<SessionInfoDto>> GetSessionsAsync(long userId, Guid? currentSessionId = null)
    {
        var sessions = await _uow.RefreshTokens.GetActiveSessionsByUserIdAsync(userId);
        return sessions.Select(s => new SessionInfoDto
        {
            SessionId = s.SessionId,
            DeviceName = s.DeviceName,
            DeviceId = s.DeviceId,
            IpAddress = s.IpAddress,
            UserAgent = s.UserAgent,
            CreatedAt = s.CreatedAt,
            LastUsedAt = s.LastUsedAt,
            ExpiresAt = s.ExpiresAt,
            IsCurrent = currentSessionId.HasValue && s.SessionId == currentSessionId.Value
        }).ToList();
    }

    public async Task RevokeSessionAsync(long userId, Guid sessionId)
    {
        var session = await _uow.RefreshTokens.GetBySessionIdAsync(userId, sessionId)
            ?? throw new InvalidOperationException("Session not found.");
        session.IsRevoked = true;
        await _uow.SaveChangesAsync();
    }

    public async Task RevokeOtherSessionsAsync(long userId, Guid currentSessionId)
    {
        var sessions = await _uow.RefreshTokens.GetActiveSessionsByUserIdAsync(userId);
        foreach (var s in sessions.Where(s => s.SessionId != currentSessionId))
            s.IsRevoked = true;
        await _uow.SaveChangesAsync();
    }

    public async Task RevokeAllSessionsAsync(long userId)
    {
        await _uow.RefreshTokens.DeleteAllForUserAsync(userId);
        await _uow.SaveChangesAsync();
    }

    // ── helpers ─────────────────────────────────────────────────────────────

    private async Task<AuthResponseDto> CreateTokensForUserAsync(
        long userId, string userName, string role,
        string? deviceName, string? deviceId,
        string? ipAddress, string? userAgent )
    {
        var accessToken = _tokenService.GenerateAccessToken(userId, userName, role);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var expiry = DateTime.UtcNow.AddDays(7);
        var sessionId = Guid.NewGuid();

        await _uow.RefreshTokens.AddAsync(new RefreshToken
        {
            UserId = userId,
            Token = refreshToken,
            ExpiresAt = expiry,
            SessionId = sessionId,
            DeviceName = deviceName,
            DeviceId = deviceId,
            IpAddress = ipAddress,
            UserAgent = userAgent
        });
        await _uow.SaveChangesAsync();

        return new AuthResponseDto
        {
            UserId = userId,
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            UserName = userName,
            Role = role,
            ExpiresAt = expiry,
            SessionId = sessionId,
           
        };
    }
}
