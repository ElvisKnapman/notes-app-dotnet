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

    public async Task<Result<UserDto>> UpdateUserAsync(Guid id, UpdateUserDto updateDto, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to update user with ID: {ID}", id);

        var userToUpdate = await _userRepo.GetByIdAsync(id, cancellationToken);

        if (userToUpdate is null)
        {
            _logger.LogWarning("No user was found with ID: {ID}", id);

            return Result<UserDto>.Fail(ErrorCodes.UserNotFound, ErrorMessages.UserNotFoundWithID);
        }

        if (!string.IsNullOrWhiteSpace(updateDto.Email) && updateDto.Email != userToUpdate.Email)
        {
            var emailExists = await _userRepo.ExistsByEmailAsync(updateDto.Email, id, cancellationToken);
            if (emailExists)
            {
                _logger.LogWarning("Update could not proceed. Email already taken.");
                return Result<UserDto>.Fail(ErrorCodes.EmailExists, ErrorMessages.EmailExists);
            }
            userToUpdate.Email = updateDto.Email;
        }

        if (!string.IsNullOrWhiteSpace(updateDto.Username) && updateDto.Username != userToUpdate.Username)
        {
            var usernameExists = await _userRepo.ExistsByUsernameAsync(updateDto.Username, id, cancellationToken);
            if (usernameExists)
            {
                _logger.LogWarning("Update could not proceed. Username already taken.");

                return Result<UserDto>.Fail(ErrorCodes.UsernameExists, ErrorMessages.UsernameExists);
            }

            userToUpdate.Username = updateDto.Username;
        }

        var changes = await _uow.SaveChangesAsync(cancellationToken);

        if (changes < 1)
        {
            _logger.LogWarning("Update did not complete successfully.");

            return Result<UserDto>.Fail(ErrorCodes.UpdateFailed, ErrorMessages.UpdateFailed);
        }

        _logger.LogInformation("Update completed successfully for user with ID: {ID}", id);


        return Result<UserDto>.Ok(userToUpdate.ToUserDto());
    }


    public async Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();

    }

    public async Task<Result<IEnumerable<UserDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _userRepo.GetAllAsync(cancellationToken);

        return Result<IEnumerable<UserDto>>.Ok(users.Select(user => user.ToUserDto()));
    }

    public async Task<Result<UserDto>> GetByEmailAsync(string email, CancellationToken cancellationToken)
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

    public async Task<Result<UserDto>> UpdateAsync(UpdateUserDto updatedUser, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
