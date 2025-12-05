using Microsoft.Extensions.Logging;
using NotesApp.Application.Common;
using NotesApp.Application.Common.Errors;
using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;
using NotesApp.Application.Mappers;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepo, ILogger<UserService> logger)
    {
        _userRepo = userRepo;
        _logger = logger;
    }

    //public async Task<Result<UserDto>> AddUserAsync(CreateUserDto userDto)
    //{
    //    _logger.LogInformation(
    //        "Attempting to create new user with email: {Email} and username {Username}",
    //        userDto.Email, userDto.Username);

    //    if (string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Username))
    //    {
    //        _logger.LogWarning("Couldn't create user, email and/or username is null or whitespace.");

    //        return Result<UserDto>.Fail(ErrorCodes.InvalidInput, ErrorMessages.InvalidInput);
    //    }

    //    // Check if user with the same email already exists in DB
    //    var userExists = await _userRepo.ExistsByEmailAndUsernameAsync(userDto.Email, userDto.Username);

    //    if (userExists)
    //    {
    //        _logger.LogWarning(
    //            "Couldn't create user. A user already exists with email: {Email} and/or username: {Username}",
    //            userDto.Email, userDto.Username);

    //        return Result<UserDto>.Fail(ErrorCodes.EmailAndOrUsernameExists, ErrorMessages.EmailAndOrUsernameExists);
    //    }

    //    var userToCreate = userDto.ToUserEntity();
    //    userToCreate.PasswordHash = await HashPassword(userDto.Password!);

    //    var createdUser = await _userRepo.AddAsync(userToCreate);

    //    _logger.LogInformation(
    //        "User successfully created with ID: {ID}",
    //        createdUser.Id);

    //    return Result<UserDto>.Ok(createdUser.ToUserDto());
    //}

    public async Task<Result<UserDto>> UpdateUserAsync(Guid id, UpdateUserDto updateDto)
    {
        _logger.LogInformation("Attempting to update user with ID: {ID}", id);

        var userToUpdate = await _userRepo.GetByIdAsync(id);

        if (userToUpdate is null)
        {
            _logger.LogWarning("No user was found with ID: {ID}", id);

            return Result<UserDto>.Fail(ErrorCodes.UserNotFound, ErrorMessages.UserNotFoundWithID);
        }

        if (!string.IsNullOrWhiteSpace(updateDto.Email) && updateDto.Email != userToUpdate.Email)
        {
            var emailExists = await _userRepo.ExistsByEmailAsync(updateDto.Email, id);
            if (emailExists)
            {
                _logger.LogWarning("Update could not proceed. Email already taken.");
                return Result<UserDto>.Fail(ErrorCodes.EmailExists, ErrorMessages.EmailExists);
            }
            userToUpdate.Email = updateDto.Email;
        }

        if (!string.IsNullOrWhiteSpace(updateDto.Username) && updateDto.Username != userToUpdate.Username)
        {
            var usernameExists = await _userRepo.ExistsByUsernameAsync(updateDto.Username, id);
            if (usernameExists)
            {
                _logger.LogWarning("Update could not proceed. Username already taken.");

                return Result<UserDto>.Fail(ErrorCodes.UsernameExists, ErrorMessages.UsernameExists);
            }

            userToUpdate.Username = updateDto.Username;
        }

        var wasUpdated = await _userRepo.UpdateAsync(userToUpdate);
        if (!wasUpdated)
        {
            _logger.LogWarning("Update did not complete successfully.");

            return Result<UserDto>.Fail(ErrorCodes.UpdateFailed, ErrorMessages.UpdateFailed);
        }

        _logger.LogInformation("Update completed successfully for user with ID: {ID}", id);


        return Result<UserDto>.Ok(userToUpdate.ToUserDto());
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

    public async Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }
}
