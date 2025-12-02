using NotesApp.Domain.Entities;

namespace NotesApp.Application.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task<bool> DeleteByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
}
