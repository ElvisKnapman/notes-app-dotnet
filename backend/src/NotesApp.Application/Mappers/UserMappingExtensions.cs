using NotesApp.Application.DTOs.Users;
using NotesApp.Domain.Entities;

namespace NotesApp.Application.Mappers;

public static class UserMappingExtensions
{
    public static UserDto ToUserDto(this User user)
    {
        return new(user.Id, user.Username, user.Email, user.CreatedAt, user.UpdatedAt);

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
