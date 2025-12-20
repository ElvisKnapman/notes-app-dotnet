using Infrastructure.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NotesApp.Application.Interfaces;
using NotesApp.Infrastructure.Security;

namespace NotesApp.Infrastructure.Auth;

public class CookieStore : ITokenStore
{
    private readonly IHttpContextAccessor _http;
    private readonly JwtOptions _jwtOptions;

    public CookieStore(IHttpContextAccessor httpContextAccessor, IOptions<JwtOptions> jwtOptions)
    {
        _http = httpContextAccessor;
        _jwtOptions = jwtOptions.Value;
    }

    public void Set(string token)
    {
        var cookieOptions = new CookieOptions
        {
            Path = "/",
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddMinutes(_jwtOptions.ExpiryInMinutes)
        };

        _http.HttpContext?.Response.Cookies.Append(CookieNames.AccessToken, token, cookieOptions);
    }

    public void Clear()
    {
        var cookieOptions = new CookieOptions
        {
            Path = "/",
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.UtcNow.AddDays(-1)
        };

        _http.HttpContext?.Response.Cookies.Append(CookieNames.AccessToken, string.Empty, cookieOptions);
    }
}
