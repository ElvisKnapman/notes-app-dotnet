using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Data.Seed;

namespace NotesApp.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db, ILogger logger)
    {
        logger.LogInformation("Starting database seeding...");

        var passwordHasher = new PasswordHasher<User>();

        // ----------------------
        // Users
        // ----------------------
        if (!await db.Users.AnyAsync())
        {
            logger.LogInformation("Seeding users...");

            foreach (var user in UserSeedData.Users)
            {
                user.PasswordHash = passwordHasher.HashPassword(user, "Password123");
                db.Users.Add(user);
            }

            await db.SaveChangesAsync();
        }

        // ----------------------
        // Notes
        // ----------------------
        if (!await db.Notes.AnyAsync())
        {
            logger.LogInformation("Seeding notes...");

            foreach (var note in NoteSeedData.Notes)
            {
                db.Notes.Add(note);
            }

            await db.SaveChangesAsync();
        }

        logger.LogInformation("Database seeding complete.");
    }
}
