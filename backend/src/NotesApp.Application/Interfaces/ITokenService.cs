using NotesApp.Application.DTOs.Users;

namespace NotesApp.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(UserDto dto);
}
