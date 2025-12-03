namespace NotesApp.Application.DTOs;

public record UserDto(Guid Id, string Username, string Email, DateTime CreatedAt, DateTime UpdatedAt);
