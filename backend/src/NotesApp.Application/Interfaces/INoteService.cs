using NotesApp.Application.Common;
using NotesApp.Application.DTOs.Common;
using NotesApp.Application.DTOs.Notes;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Interfaces;

public interface INoteService
{
    Task<Result<IEnumerable<NoteDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<PagedResult<NoteDto>>> GetNotesForUserAsync(
        Guid userId,
        NoteQueryDto queryDto,
        CancellationToken cancellationToken = default);

    Task<Result<NoteDto>> GetByIdAsync(Guid noteId, CancellationToken cancellationToken = default);
    Task<Result<NoteDto>> UpdateAsync(
        UpdateNoteDto updateDto,
        Guid userId,
        CancellationToken cancellationToken = default
    );

    Task<Result<NoteDto>> CreateAsync(CreateNoteDto createNoteDto, CancellationToken cancellationToken = default);
    Task<Note?> GetEntityByIdAsync(Guid noteId, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(Guid noteId, Guid userId, CancellationToken cancellationToken = default);
}
