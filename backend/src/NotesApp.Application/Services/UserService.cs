using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;
using NotesApp.Application.Mappers;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Services;

public class UserService : IUserService
{
    private static List<User> _users = [];

    private readonly IUserRepository _userRepo;

    public UserService(IUserRepository userRepo)
    {
        _userRepo = userRepo;
    }

    public async Task<UserDto> AddUserAsync(CreateUserDto userDto)
    {
        _users.Add(userDto.ToUserEntity());
        return userDto.ToUserDto();
    }

    public async Task<bool> DeleteByUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        return _users.Select(user => user.ToUserDto());
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        return _users.Find(u => u.Id == id)?.ToUserDto();
    }

    public async Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}
