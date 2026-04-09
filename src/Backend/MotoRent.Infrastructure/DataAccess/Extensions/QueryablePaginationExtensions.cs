using Microsoft.EntityFrameworkCore;
using MotoRent.Domain.Pagination;

namespace MotoRent.Infrastructure.DataAccess.Extensions;

public static class QueryablePaginationExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        PageRequest pageRequest,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(pageRequest.Skip)
            .Take(pageRequest.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<T>(items, pageRequest.PageNumber, pageRequest.PageSize, totalCount);
    }
}
