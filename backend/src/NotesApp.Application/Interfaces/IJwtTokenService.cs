using NotesApp.Application.DTOs.Users;

namespace NotesApp.Application.Interfaces;

public interface IJwtTokenService
{
    string GenerateToken(UserDto dto);
}
