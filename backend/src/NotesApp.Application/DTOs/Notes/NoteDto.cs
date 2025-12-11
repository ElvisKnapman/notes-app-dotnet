namespace NotesApp.Application.DTOs.Notes;

public record NoteDto(
    Guid Id,
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Guid UserId
);
