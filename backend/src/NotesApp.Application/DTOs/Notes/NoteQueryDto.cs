namespace NotesApp.Application.DTOs.Notes;

public record NoteQueryDto(
    int PageSize,
    int PageNumber,
    string? SortBy,
    bool Descending,
    string? Search
);
