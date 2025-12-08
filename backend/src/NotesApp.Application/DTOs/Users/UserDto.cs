namespace NotesApp.Application.DTOs.Users;

public record UserDto(Guid Id, string Username, string Email, DateTime CreatedAt, DateTime UpdatedAt);
