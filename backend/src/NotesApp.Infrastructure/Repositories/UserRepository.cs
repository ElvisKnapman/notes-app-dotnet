using Microsoft.EntityFrameworkCore;
using NotesApp.Application.Interfaces;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Data;

namespace NotesApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _ctx;

    public UserRepository(AppDbContext context)
    {
        _ctx = context;
    }

    public void Add(User user)
    {
        _ctx.Users.Add(user);
    }

    public void Update(User user)
    {
        _ctx.Users.Update(user);
    }

    public void Delete(User user)
    {
        _ctx.Users.Remove(user);
    }

    public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _ctx.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _ctx.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsByEmailAsync(
        string email, Guid id, CancellationToken cancellationToken = default)
    {
        return await _ctx.Users.AnyAsync(u => u.Email == email && u.Id != id);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _ctx.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<bool> ExistsByUsernameAsync(
        string username, Guid id, CancellationToken cancellationToken = default)
    {
        return await _ctx.Users.AnyAsync(u => u.Username == username && u.Id != id);
    }

    public async Task<bool> ExistsByEmailAndUsernameAsync(
        string email, string username, CancellationToken cancellationToken = default)
    {
        return await _ctx.Users.AnyAsync(u => u.Email == email || u.Username == username);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _ctx.Users.ToListAsync();
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByEmailNoTrackingAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _ctx.Users.FindAsync(id);
    }

    public async Task<User?> GetByIdNoTrackingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }
}
