using Infrastructure.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Infrastructure.Security;
using System.Text;

namespace NotesApp.Api.Extensions;

public static class ServiceConfigurationExtensions
{
    public static IServiceCollection AddJwtCookieAuthentication(
        this IServiceCollection services,
        IConfiguration config
    )
    {
        // Register authorization handlers and requirements here
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var jwtOptions = config.GetSection(nameof(JwtOptions)).Get<JwtOptions>()
        ?? throw new InvalidOperationException($"{nameof(JwtOptions)} section is missing");

            if (string.IsNullOrWhiteSpace(jwtOptions.SecretKey))
                throw new ArgumentException($"JWT {nameof(JwtOptions.SecretKey)} must be provided", nameof(jwtOptions.SecretKey));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));

            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,


                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = securityKey,

                // Strict JWT expiration time (no grace period from server)
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Cookies[CookieNames.AccessToken];

                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Token = token;
                    }

                    return Task.CompletedTask;
                }
            };
        });
        return services;
    }
}
