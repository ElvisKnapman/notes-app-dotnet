using NotesApp.Application.Common;
using NotesApp.Application.DTOs;

namespace NotesApp.Application.Interfaces;

public interface IAuthService
{
    Task<Result<UserDto>> RegisterUserAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
    Task<Result<UserDto>> LoginAsync(LoginUserDto dto, CancellationToken cancellationToken);
}
