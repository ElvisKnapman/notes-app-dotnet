namespace NotesApp.Application.DTOs.Common;

public class PagedResult<T>
{
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public IReadOnlyCollection<T> Items { get; set; } = [];

    public PagedResult(int totalCount, int pageNumber, int pageSize, IReadOnlyCollection<T> items)
    {
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        Items = items;
    }
}
