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
    Task<bool> DeleteByIdAsync(Guid id);
    Task<bool> ExistsByIdAsync(Guid id);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email, Guid id);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<bool> ExistsByUsernameAsync(string username, Guid id);
    Task<bool> ExistsByEmailAndUsernameAsync(string email, string username);
    Task<bool> UpdateAsync(User user);
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken);
}
