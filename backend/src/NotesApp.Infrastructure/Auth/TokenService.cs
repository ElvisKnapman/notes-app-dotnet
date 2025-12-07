using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;
using NotesApp.Infrastructure.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NotesApp.Infrastructure.Auth;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;

    public TokenService(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public string GenerateToken(UserDto dto)
    {
        // Do the claims
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, dto.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, dto.Email),
            new Claim("username", dto.Username)
        };

        // Do the key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

        // Do the creds
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Do the token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = creds,
            Expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes),
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
        };

        // Generate the token
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Return the token string
        return tokenHandler.WriteToken(token);
    }
}
