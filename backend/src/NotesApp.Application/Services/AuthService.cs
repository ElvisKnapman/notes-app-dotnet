using NotesApp.Application.Common;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;
using NotesApp.Application.Mappers;

namespace NotesApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _uow;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public AuthService(IUserRepository userRepository, IPasswordService passwordService, IUnitOfWork uow)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _uow = uow;
    }

    public async Task<Result<UserDto>> RegisterUserAsync(
        CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        // Check if user with email already exists
        var userExists = await _userRepository.ExistsByEmailAsync(createUserDto.Email);

        if (userExists)
        {
            return Result<UserDto>.Fail(ErrorCodes.EmailExists, ErrorMessages.EmailExists);
        }

        // Create entity and hash password
        var userToCreate = createUserDto.ToUserEntity();

        userToCreate.PasswordHash = _passwordService.HashPassword(userToCreate, createUserDto.Password);

        // Add the user to the EF tracker for insert
        _userRepository.Add(userToCreate);

        // Save user to database
        var changes = await _uow.SaveChangesAsync(cancellationToken);


        return changes > 0 ? Result<UserDto>.Ok(userToCreate.ToUserDto())
            : Result<UserDto>.Fail(ErrorCodes.CreationFailed, ErrorMessages.CreationFailed);

    }
}
