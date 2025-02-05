using invoice_system.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Quotation.Queries.GetQuotations;

public class GetQuotationsQueryHandler : IRequestHandler<GetQuotationsQuery, List<Models.Quotations>>
{
    private readonly Db _db;

    public GetQuotationsQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<List<Models.Quotations>> Handle(GetQuotationsQuery request, CancellationToken ct)
    {
        var query = _db.Quotations
            .Include(q => q.Customer)
            .Include(q => q.Items)
            .Include(q => q.Users)
            .AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(q => q.UserId == request.UserId);
        }

        if (request.CustomerId.HasValue)
        {
            query = query.Where(q => q.CustomerId == request.CustomerId);
        }

        if (!string.IsNullOrEmpty(request.Status))
        {
            query = query.Where(q => q.Status == request.Status);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(q => q.Date >= request.StartDate);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(q => q.Date <= request.EndDate);
        }

        return await query.ToListAsync(ct);
    }
}