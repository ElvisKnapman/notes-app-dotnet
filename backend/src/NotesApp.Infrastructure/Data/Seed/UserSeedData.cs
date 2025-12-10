using NotesApp.Domain.Entities;

namespace NotesApp.Infrastructure.Data.Seed;

public static class UserSeedData
{
    public static List<User> Users => new()
    {
        new User
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
            Email = "alice@example.com",
            Username = "alice"
        },
        new User
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
            Email = "bob@example.com",
            Username = "bobbo"
        },
        new User
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"),
            Email = "richardclark@gmail.com",
            Username = "richardclark"
        },
        new User
        {
            Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
            Email = "knappers@gmail.com",
            Username = "knappers"
        },
    };
}
