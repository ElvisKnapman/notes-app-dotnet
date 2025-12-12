using NotesApp.Application.DTOs.Notes;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Mappers;

public static class NoteMappingExtensions
{
    public static NoteDto ToNoteDto(this Note note)
    {
        return new(note.Id, note.Title, note.Content, note.CreatedAt, note.UpdatedAt, note.UserId);
    }

    public static Note ToNoteEntity(this CreateNoteDto createNoteDto)
    {
        return new Note()
        {
            Title = createNoteDto.Title,
            Content = createNoteDto.Content,
            UserId = createNoteDto.NoteOwnerId
        };
    }

    public static Note UpdateNoteEntity(this Note note, UpdateNoteDto updateNoteDto)
    {
        if (!string.IsNullOrWhiteSpace(updateNoteDto.Title))
        {
            note.Title = updateNoteDto.Title;
        }

        if (!string.IsNullOrWhiteSpace(updateNoteDto.Content))
        {
            note.Content = updateNoteDto.Content;
        }

        return note;
    }
}
