namespace NotesApp.Infrastructure.Security;

public record JwtOptions
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string SecreyKey { get; init; } = string.Empty;
    public int ExpiryInMinutes { get; init; } = 30;
}
