using NotesApp.Application.DTOs;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Mappers;

public static class UserMappingExtensions
{
    public static UserDto ToUserDto(this User user)
    {
        return new()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }

    public static UserDto ToUserDto(this CreateUserDto userDto)
    {
        return new()
        {
            Username = userDto.Username ?? "",
            Email = userDto.Email ?? ""
        };
    }

    public static User ToUserEntity(this CreateUserDto userDto)
    {
        return new()
        {
            Username = userDto.Username ?? "",
            Email = userDto.Email ?? ""
        };
    }
}
