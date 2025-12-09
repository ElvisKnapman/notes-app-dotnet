using Microsoft.Extensions.Logging;
using NotesApp.Application.Common;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs.Users;
using NotesApp.Application.Interfaces;
using NotesApp.Application.Mappers;

namespace NotesApp.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _uow;
    private readonly IUserRepository _userRepo;
    private readonly ILogger<UserService> _logger;

    public UserService(IUnitOfWork uow, IUserRepository userRepo, ILogger<UserService> logger)
    {
        _uow = uow;
        _userRepo = userRepo;
        _logger = logger;
    }


    public async Task<Result<UserDto>> UpdateAsync(
        Guid id, UpdateUserDto updateDto,
        CancellationToken cancellationToken = default
    )
    {
        _logger.LogInformation("Attempting to update user with ID: {ID}", id);

        var userToUpdate = await _userRepo.GetByIdAsync(id, cancellationToken);

        if (userToUpdate is null)
        {
            _logger.LogWarning("No user was found with ID: {ID}", id);

            return Result<UserDto>.Fail(ErrorCodes.UserNotFound, ErrorMessages.UserNotFoundWithID);
        }

        // Verify there are actual changes to be made
        if (
            (string.IsNullOrWhiteSpace(updateDto.Email) ||
             string.Equals(updateDto.Email, userToUpdate.Email, StringComparison.OrdinalIgnoreCase)) &&
            (string.IsNullOrWhiteSpace(updateDto.Username) ||
             string.Equals(updateDto.Username, userToUpdate.Username, StringComparison.OrdinalIgnoreCase))
 )
        {
            _logger.LogInformation("No changes detected for user with ID: {ID}, updated canceled.", id);

            return Result<UserDto>.Ok(userToUpdate.ToUserDto());
        }

        // Check for email uniqueness if it's being changed
        if (!string.IsNullOrWhiteSpace(updateDto.Email))
        {
            var emailExists = await _userRepo.ExistsByEmailAsync(updateDto.Email, id, cancellationToken = default);

            if (emailExists)
            {
                _logger.LogWarning("Update could not proceed. Email already taken.");

                return Result<UserDto>.Fail(ErrorCodes.EmailExists, ErrorMessages.EmailExists);
            }
        }

        // Check for username uniqueness if it's being changed
        if (!string.IsNullOrWhiteSpace(updateDto.Username))
        {
            var usernameExists = await _userRepo.ExistsByUsernameAsync(updateDto.Username, id, cancellationToken);

            if (usernameExists)
            {
                _logger.LogWarning("Update could not proceed. Username already taken.");

                return Result<UserDto>.Fail(ErrorCodes.UsernameExists, ErrorMessages.UsernameExists);
            }
        }

        // Map updated value(s) to entity
        userToUpdate.UpdateUserEntity(updateDto);

        // User entity already tracked by EF Core (when retrieved), so just save tracked changes
        var changes = await _uow.SaveChangesAsync(cancellationToken);

        if (changes < 1)
        {
            _logger.LogWarning("Update did not complete successfully while attempting to save.");

            return Result<UserDto>.Fail(ErrorCodes.UpdateFailed, ErrorMessages.UpdateFailed);
        }

        _logger.LogInformation("Update completed successfully for user with ID: {ID}", id);


        return Result<UserDto>.Ok(userToUpdate.ToUserDto());
    }


    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();

    }

    public async Task<Result<IEnumerable<UserDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var users = await _userRepo.GetAllAsync(cancellationToken);

        return Result<IEnumerable<UserDto>>.Ok(users.Select(user => user.ToUserDto()));
    }

    public async Task<Result<UserDto>> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to retrieve user with email: {Email}", email);

        var user = await _userRepo.GetByEmailAsync(email, cancellationToken);

        if (user is null)
        {
            _logger.LogWarning("No user was found with email: {Email}", email);

            return Result<UserDto>.Fail(ErrorCodes.UserNotFound, ErrorMessages.UserNotFoundWithEmail);
        }

        _logger.LogInformation("User successfully found with email: {Email}", email);

        return Result<UserDto>.Ok(user.ToUserDto());
    }

    public async Task<Result<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to retrieve user with ID: {ID}", id);

        var user = await _userRepo.GetByIdAsync(id, cancellationToken);

        if (user is null)
        {
            _logger.LogWarning("No user was found with ID: {ID}", id);

            return Result<UserDto>.Fail(ErrorCodes.UserNotFound, ErrorMessages.UserNotFoundWithID);
        }

        _logger.LogInformation("User successfully found with ID: {ID}", id);

        return Result<UserDto>.Ok(user.ToUserDto());
    }
}
