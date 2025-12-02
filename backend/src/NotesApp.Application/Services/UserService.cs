using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;
using NotesApp.Application.Mappers;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepo, IPasswordHasher passwordHasher)
    {
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDto> AddUserAsync(CreateUserDto userDto)
    {
        var user = await _userRepo.AddAsync(userDto.ToUserEntity());
        return userDto.ToUserDto();
    }

    public async Task<bool> DeleteByUserAsync(Guid id)
    {
        return await _userRepo.DeleteByIdAsync(id);

    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepo.GetAllAsync();

        return users.Select(user => user.ToUserDto());
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = await _userRepo.GetByEmailAsync(email);

        return user?.ToUserDto();
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepo.GetByIdAsync(id);

        return user?.ToUserDto();
    }

    public async Task<string> HashPassword(string password)
    {
        return _passwordHasher.Hash(password);
    }

    public async Task<bool> VerifyPassword(string password, string hashedPassword)
    {
        return _passwordHasher.Verify(password, hashedPassword);
    }

    public async Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}
