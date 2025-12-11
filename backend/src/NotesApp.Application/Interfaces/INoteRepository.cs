using NotesApp.Domain.Entities;

namespace NotesApp.Application.Interfaces;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Note?> GetByIdNoTrackingAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(Note note);
    void Update(Note updatedNote);
}
