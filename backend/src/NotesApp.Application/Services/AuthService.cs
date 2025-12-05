using NotesApp.Application.Common;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;
using NotesApp.Application.Mappers;

namespace NotesApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public AuthService(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<Result<UserDto>> RegisterUserAsync(
        CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(createUserDto.Email))
        {
            return Result<UserDto>.Fail(ErrorCodes.InvalidInput, ErrorMessages.InvalidEmailInput);
        }

        if (string.IsNullOrWhiteSpace(createUserDto.Password))
        {
            return Result<UserDto>.Fail(ErrorCodes.InvalidInput, ErrorMessages.InvalidPasswordInput);
        }

        // Check if user with email already exists
        var userExists = await _userRepository.ExistsByEmailAsync(createUserDto.Email);

        if (userExists)
        {
            return Result<UserDto>.Fail(ErrorCodes.EmailExists, ErrorMessages.EmailExists);
        }

        // Create entity and hash password
        var userToCreate = createUserDto.ToUserEntity();

        userToCreate.PasswordHash = _passwordService.HashPassword(userToCreate, createUserDto.Password);

        // Create the user
        var user = await _userRepository.AddAsync(userToCreate);

        // Save user to database
        var saveSuccessful = await _userRepository.SaveChangesAsync(cancellationToken);


        return saveSuccessful ? Result<UserDto>.Ok(user.ToUserDto())
            : Result<UserDto>.Fail(ErrorCodes.CreationFailed, ErrorMessages.CreationFailed);

    }
}
