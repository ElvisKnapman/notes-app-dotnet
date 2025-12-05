using NotesApp.Application.Common;
using NotesApp.Application.DTOs;

namespace NotesApp.Application.Interfaces;

public interface IUserService
{
    Task<UserDto?> GetUserByIdAsync(Guid id);
    Task<UserDto?> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<bool> DeleteByUserAsync(Guid id);
    //Task<Result<UserDto>> AddUserAsync(CreateUserDto userDto);
    Task<Result<UserDto>> UpdateUserAsync(Guid id, UpdateUserDto updateDto);
}
