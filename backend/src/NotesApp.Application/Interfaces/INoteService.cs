using NotesApp.Application.Common;
using NotesApp.Application.DTOs.Notes;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Interfaces;

public interface INoteService
{
    Task<Result<IEnumerable<NoteDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<NoteDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<NoteDto>> UpdateAsync(
        Note note,
        UpdateNoteDto updateDto,
        CancellationToken cancellationToken = default
    );
    Task<Result<NoteDto>> CreateAsync(CreateNoteDto createNoteDto, CancellationToken cancellationToken = default);
    Task<Note?> GetEntityByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
