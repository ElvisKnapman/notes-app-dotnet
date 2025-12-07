using NotesApp.Application.Common;
using NotesApp.Application.DTOs;

namespace NotesApp.Application.Interfaces;

public interface IUserService
{
    Task<Result<UserDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<UserDto>> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Result<IEnumerable<UserDto>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<bool>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task<Result<UserDto>> UpdateAsync(UpdateUserDto updateDto, CancellationToken cancellationToken = default);
}
