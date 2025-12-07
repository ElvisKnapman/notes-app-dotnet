using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NotesApp.Api.Filters;
using NotesApp.Application.Configuration;
using NotesApp.Infrastructure.Configuration;
using NotesApp.Infrastructure.Security;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ValidationActionFilter>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationActionFilter>();
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Add Application layer services
builder.Services.AddApplication();

// Add Infrastructure layer services
string connectionString = builder.Configuration.GetConnectionString("DBConnection") ??
    throw new InvalidOperationException("No connection string found in config");
builder.Services.AddInfrastructure(connectionString);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtOptions = builder.Configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>()
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


            ValidIssuer = jwtOptions?.Issuer,
            ValidAudience = jwtOptions?.Audience,
            IssuerSigningKey = securityKey,

            // Strict JWT expiration time (no grace period from server)
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
