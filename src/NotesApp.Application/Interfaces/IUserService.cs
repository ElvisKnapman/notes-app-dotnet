using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllUsersAsync();
    Task<bool> DeleteByUserAsync(Guid id);
    Task UpdateUserAsync(User user);
    Task<User> AddUserAsync(CreateUserDto userDto);
}
