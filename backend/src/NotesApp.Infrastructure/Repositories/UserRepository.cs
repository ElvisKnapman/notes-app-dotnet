using NotesApp.Application.Interfaces;
using NotesApp.Domain.Entities;

namespace NotesApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private static List<User> _users = [];

    public async Task<User> AddAsync(User user)
    {
        _users.Add(user);
        return user;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var user = _users.Find(u => u.Id == id);

        if (user is null) return false;

        _users.Remove(user);
        return true;
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return _users.Any(u => u.Id == id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return _users.ToList();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return _users.Find(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return _users.Find(u => u.Id == id);
    }

    public async Task UpdateAsync(User user)
    {
        var index = _users.FindIndex(u => u.Id == user.Id);

        _users.RemoveAt(index);
        _users.Add(user);
    }
}
