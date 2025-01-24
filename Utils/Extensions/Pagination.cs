using invoice_system.Utils.Helpers.ResHelpers;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Utils.Extensions;

public static class Paginations {
    public static async Task<ApiResponse<List<T>>> usePaginate<T , TSource>(IQueryable<TSource> query , int Page , int PageSize , CancellationToken ct = default){
        var totalCount = await query.CountAsync();
        var items = await query.Skip((Page - 1) * PageSize).Take(PageSize).ToListAsync(ct);

        return ResHelper.Success(items.Adapt<List<T>>() , new PaginateResponse {
            totalcounts = totalCount,
            page = Page,
            pagesizes = PageSize,
            totalpages = (int)Math.Ceiling(totalCount / (double)PageSize)
        });
    }
}