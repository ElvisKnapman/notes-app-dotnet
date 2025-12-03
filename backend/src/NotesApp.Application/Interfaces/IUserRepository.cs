using NotesApp.Domain.Entities;

namespace NotesApp.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByIdNoTrackingAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByEmailNoTrackingAsync(string email);
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task<bool> DeleteByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsAsync(string email);
    Task<bool> ExistsAsync(string email, string username);
}
