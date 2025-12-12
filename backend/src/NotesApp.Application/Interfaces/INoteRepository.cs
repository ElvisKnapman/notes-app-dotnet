using NotesApp.Domain.Entities;

namespace NotesApp.Application.Interfaces;

public interface INoteRepository
{
    Task<IEnumerable<Note>> GetAllAsync(CancellationToken cancellationToken = default);
    IQueryable<Note> GetAllQueryable();
    Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Note?> GetByIdNoTrackingAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);
    bool HasChanges(Note note);
    void Add(Note note);
    void Update(Note updatedNote);
}
