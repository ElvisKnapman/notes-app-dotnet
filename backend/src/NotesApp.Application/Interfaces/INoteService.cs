using NotesApp.Application.Common;
using NotesApp.Application.DTOs.Notes;

namespace NotesApp.Application.Interfaces;

public interface INoteService
{
    Task<Result<NoteDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<NoteDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<NoteDto>> CreateAsync(CreateNoteDto createNoteDto, CancellationToken cancellationToken = default);
}
