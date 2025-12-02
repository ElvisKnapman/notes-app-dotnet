namespace NotesApp.Domain.Entities;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public ICollection<Note> Notes { get; set; } = new List<Note>();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
