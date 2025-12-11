using Microsoft.Extensions.DependencyInjection;
using NotesApp.Application.Interfaces;
using NotesApp.Application.Services;

namespace NotesApp.Application.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register everything for Application layer here
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<INoteService, NoteService>();

        return services;
    }
}
