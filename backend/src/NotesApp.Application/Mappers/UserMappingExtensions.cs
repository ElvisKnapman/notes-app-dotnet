using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class UserMappingExtensions
{
    public static UserDto ToUserDto(this User user)
    {
        return new()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email
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
