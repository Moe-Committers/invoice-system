using invoice_system.Utils.Helpers.ResHelpers;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Utils.Extensions;

public static class Paginations
{
    public static async Task<ApiResponse<List<T>>> UsePaginate<T, TSource>(
        this IQueryable<TSource> query,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default
    )
    {
        var totalCount = await query.CountAsync(ct);
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return ResHelper.Success(items.Adapt<List<T>>(), new PaginateResponse
        {
            totalcounts = totalCount,
            page = pageNumber,
            pagesizes = pageSize,
            totalpages = (int)Math.Ceiling(totalCount / (double)pageSize)
        });
    }
}