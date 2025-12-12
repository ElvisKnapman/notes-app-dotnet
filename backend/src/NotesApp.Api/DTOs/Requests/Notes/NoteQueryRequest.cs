namespace NotesApp.Api.DTOs.Requests.Notes;

public record NoteQueryRequest(
    int PageSize = 10,
    int PageNumber = 1,
    string? SortBy = null,
    bool Descending = false,
    string? Search = null
);
