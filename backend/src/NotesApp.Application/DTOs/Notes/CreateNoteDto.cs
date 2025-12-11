namespace NotesApp.Application.DTOs.Notes;

public record CreateNoteDto(Guid NoteOwnerId, string Title, string Content);
