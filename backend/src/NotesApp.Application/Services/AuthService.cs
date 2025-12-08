using Microsoft.Extensions.Logging;
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
    private readonly ILogger _logger;

    public AuthService(
        IUserRepository userRepository,
        IPasswordService passwordService,
        IUnitOfWork uow,
        ILogger<AuthService> logger
        )
    {
        _logger = logger;
        _userRepository = userRepository;
        _passwordService = passwordService;
        _uow = uow;
    }

    public async Task<Result<UserDto>> RegisterUserAsync(
        CreateUserDto createUserDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering new user with email: {Email}", createUserDto.Email);

        // Check if user with email already exists
        var emailExists = await _userRepository.ExistsByEmailAsync(createUserDto.Email, cancellationToken);

        if (emailExists)
        {
            _logger.LogWarning("Registration failed: Email {Email} is already taken.", createUserDto.Email);
            return Result<UserDto>.Fail(ErrorCodes.EmailExists, ErrorMessages.EmailExists);
        }

        var usernameExists = await _userRepository.ExistsByUsernameAsync(createUserDto.Username, cancellationToken);

        if (usernameExists)
        {
            _logger.LogWarning("Registration failed: Username {Username} is already taken.", createUserDto.Username);
            return Result<UserDto>.Fail(ErrorCodes.UsernameExists, ErrorMessages.UsernameExists);
        }

        // Create the user entity
        var userToCreate = createUserDto.ToUserEntity();

        userToCreate.PasswordHash = _passwordService.HashPassword(userToCreate, createUserDto.Password);

        // Add the user to the EF tracker for insert
        _userRepository.Add(userToCreate);

        // Save user to database
        _logger.LogInformation("Saving new user to the database.");
        var changes = await _uow.SaveChangesAsync(cancellationToken);


        if (changes == 0)
        {
            _logger.LogError("User registration failed for email: {Email}", createUserDto.Email);

            return Result<UserDto>.Fail(ErrorCodes.CreationFailed, ErrorMessages.CreationFailed);
        }

        _logger.LogInformation("User registered successfully with ID: {ID}", userToCreate.Id);
        return Result<UserDto>.Ok(userToCreate.ToUserDto());
    }

    public async Task<Result<UserDto>> LoginAsync(
        LoginUserDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to log in user with email: {Email}", dto.Email);

        var user = await _userRepository.GetByEmailAsync(dto.Email, cancellationToken);

        if (user is null)
        {
            _logger.LogWarning("User with email: {Email} is not a registered user. Login failed.", dto.Email);

            return Result<UserDto>.Fail(ErrorCodes.UserNotFound, ErrorMessages.UserNotFound);
        }

        _logger.LogInformation("User found, ID is: {ID}", user.Id);

        // Validate password
        var passwordsMatch = _passwordService.VerifyPassword(user, user.PasswordHash, dto.Password);

        if (!passwordsMatch)
        {
            _logger.LogWarning(
                "Passwords do not match. User ID: {ID} failed authentication.", user.Id
                );

            return Result<UserDto>.Fail(ErrorCodes.InvalidCredentials, ErrorMessages.InvalidCredentials);
        }

        _logger.LogInformation(
            "Password successfully verified. User with ID: {ID} passed authentication", user.Id
            );

        return Result<UserDto>.Ok(user.ToUserDto());
    }
}
