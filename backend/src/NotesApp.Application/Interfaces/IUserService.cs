using NotesApp.Application.DTOs;
using NotesApp.Domain.Common;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<bool> DeleteByUserAsync(Guid id);
    Task UpdateUserAsync(User user);
    Task<Result<UserDto>> AddUserAsync(CreateUserDto userDto);
    Task<string> HashPassword(string password);
    Task<bool> VerifyPassword(string password, string hashedPassword);
}
