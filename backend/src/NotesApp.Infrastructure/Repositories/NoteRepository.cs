using Microsoft.EntityFrameworkCore;
using NotesApp.Application.Interfaces;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Data;

namespace NotesApp.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _context;

    public NoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public void Add(Note note)
    {
        _context.Add(note);
    }

    public void Update(Note updatedNote)
    {
        _context.Update(updatedNote);
    }

    public async Task<IEnumerable<Note>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Notes.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Note?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Notes.FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }

    public async Task<Note?> GetByIdNoTrackingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Notes.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Notes.AnyAsync(n => n.Id == id, cancellationToken);
    }

}
