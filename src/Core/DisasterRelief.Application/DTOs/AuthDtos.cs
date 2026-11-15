using DisasterRelief.Domain.Enums;

namespace DisasterRelief.Application.DTOs;

public sealed record RegisterRequest(string FullName, string Email, string Password);
public sealed record LoginRequest(string Email, string Password);
public sealed record AuthResponse(Guid UserId, string FullName, string Email, UserRole Role, string AccessToken);