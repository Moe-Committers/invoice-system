using Microsoft.EntityFrameworkCore;

namespace invoice_system.Utils.Extensions;

public static class Paginations {
    public static async Task<List<T>> usePaginate<T , TSource>(IQueryable<TSource> query , int Page , int PageSize , CancellationToken ct){
        var totalCount = await query.CountAsync();
        var items = await query.Skip((Page - 1) * PageSize).Take(PageSize).ToListAsync();

        return 
    }
}