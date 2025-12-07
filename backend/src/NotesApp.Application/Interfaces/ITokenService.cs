using NotesApp.Application.DTOs;

namespace NotesApp.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(UserDto dto);
}
