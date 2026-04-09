namespace MotoRent.Domain.Pagination;

public sealed record PageRequest
{
    private const int DefaultPageNumber = 1;
    private const int DefaultPageSize = 10;

    public int PageNumber { get; }
    public int PageSize { get; }
    public int Skip => (PageNumber - 1) * PageSize;

    public PageRequest(int pageNumber = DefaultPageNumber, int pageSize = DefaultPageSize)
    {
        PageNumber = pageNumber < DefaultPageNumber ? DefaultPageNumber : pageNumber;
        PageSize = pageSize < 1 ? DefaultPageSize : pageSize;
    }
}
