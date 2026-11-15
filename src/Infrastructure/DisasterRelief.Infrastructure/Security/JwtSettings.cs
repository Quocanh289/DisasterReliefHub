namespace DisasterRelief.Infrastructure.Security;

public sealed class JwtSettings
{
    public const string SectionName = "Jwt";
    public string Issuer { get; init; } = "DisasterReliefCoordination";
    public string Audience { get; init; } = "DisasterReliefCoordination.Client";
    public string Key { get; init; } = string.Empty;
    public int ExpiryMinutes { get; init; } = 60;
}