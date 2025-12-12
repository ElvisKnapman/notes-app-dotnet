using Microsoft.EntityFrameworkCore;
using NotesApp.Application.DTOs.Common;
using NotesApp.Application.DTOs.Notes;
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

    public async Task<PagedResult<Note>> QueryUserNotesAsync(
        Guid userId,
        NoteQueryDto queryDto,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Notes.AsNoTracking().Where(n => n.UserId == userId);

        if (!string.IsNullOrWhiteSpace(queryDto.Search))
        {
            query = query.Where(n => n.Title.Contains(queryDto.Search) || n.Content.Contains(queryDto.Search));
        }

        if (!string.IsNullOrWhiteSpace(queryDto.SortBy))
        {
            query = queryDto.SortBy.ToLower() switch
            {
                "title" => queryDto.Descending
                    ? query.OrderByDescending(n => n.Title)
                    : query.OrderBy(n => n.Title),
                "updatedAt" => queryDto.Descending
                    ? query.OrderByDescending(n => n.CreatedAt)
                    : query.OrderBy(n => n.CreatedAt),
                _ => queryDto.Descending
                    ? query.OrderByDescending(n => n.CreatedAt)
                    : query.OrderBy(n => n.CreatedAt),
            };
        }

        // Total count BEFORE paging
        var totalItems = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((queryDto.PageNumber - 1) * queryDto.PageSize)
            .Take(queryDto.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Note>(totalItems, queryDto.PageNumber, queryDto.PageSize, items);
    }

    public async Task<IEnumerable<Note>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Notes.AsNoTracking().ToListAsync(cancellationToken);
    }

    public IQueryable<Note> GetAllQueryable()
    {
        return _context.Notes.AsNoTracking();
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

    public bool HasChanges(Note note)
    {
        var properties = _context.Entry(note).Properties;

        return _context.Entry(note).Properties.Any(p => p.IsModified);
    }
}
