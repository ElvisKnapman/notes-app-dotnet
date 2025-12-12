namespace NotesApp.Application.DTOs.Notes;

public record UpdateNoteDto(Guid Id, string? Title, string? Content);
