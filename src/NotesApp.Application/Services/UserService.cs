using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class UserService : IUserService
{
    private static List<User> _users = [];

    public Task<User> AddUserAsync(User user)
    {
        
    }

    public Task<bool> DeleteByUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllUsersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByIdAsync(Guid id)
    {
        
    }

    public Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}
