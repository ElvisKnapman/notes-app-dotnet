using NotesApp.Domain.Entities;

namespace NotesApp.Infrastructure.Data.Seed;

public static class NoteSeedData
{
    public static List<Note> Notes => new()
    {
        new Note
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1"),
            UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1"),
            Title = "Alice’s First Note",
            Content = "This is a sample note for Alice."
        },
        new Note
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2"),
            UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2"),
            Title = "Bob’s Quick Note",
            Content = "Bob wrote this quick seeded note."
        },
        new Note
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3"),
            UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3"),
            Title = "Richard's Reminder",
            Content = "Reminder for Richard."
        },
        new Note
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4"),
            UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
            Title = "Knappers' Note",
            Content = "A note for Knappers."
        },
        new Note
        {
            Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5"),
            UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4"),
            Title = "Knappers' Second Note",
            Content = "Another note for Knappers."
        },
    };
}
