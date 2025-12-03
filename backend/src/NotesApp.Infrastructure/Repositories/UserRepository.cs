using Microsoft.EntityFrameworkCore;
using NotesApp.Application.Interfaces;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Persistence;

namespace NotesApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _ctx;

    public UserRepository(AppDbContext context)
    {
        _ctx = context;
    }

    public async Task<User> AddAsync(User user)
    {
        await _ctx.Users.AddAsync(user);
        await _ctx.SaveChangesAsync();

        return user;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var user = await _ctx.Users.FindAsync(id);

        if (user is null) return false;

        _ctx.Users.Remove(user);
        await _ctx.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await _ctx.Users.AnyAsync(u => u.Id == id);
    }

    public async Task<bool> ExistsByEmailAsync(string email)     
    {
        return await _ctx.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> ExistsByEmailAsync(string email, Guid id)
    {
        return await _ctx.Users.AnyAsync(u => u.Email == email && u.Id != id);
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await _ctx.Users.AnyAsync(u => u.Username == username);
    }

    public async Task<bool> ExistsByUsernameAsync(string username, Guid id)
    {
        return await _ctx.Users.AnyAsync(u => u.Username == username && u.Id != id);
    }

    public async Task<bool> ExistsByEmailAndUsernameAsync(string email, string username)
    {
        return await _ctx.Users.AnyAsync(u => u.Email == email || u.Username == username);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _ctx.Users.ToListAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByEmailNoTrackingAsync(string email)
    {
        return await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _ctx.Users.FindAsync(id);
    }

    public async Task<User?> GetByIdNoTrackingAsync(Guid id)
    {
        return await _ctx.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        var affectedRows = await _ctx.SaveChangesAsync();

        return affectedRows > 0;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var result = _ctx.Users.Update(user);

        return await _ctx.SaveChangesAsync() > 0;
    }
}
