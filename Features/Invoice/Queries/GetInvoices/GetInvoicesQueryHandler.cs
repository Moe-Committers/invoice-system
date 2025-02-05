using invoice_system.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Invoice.Queries.GetInvoices;

public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, List<Models.Invoices>>
{
    private readonly Db _db;

    public GetInvoicesQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<List<Models.Invoices>> Handle(GetInvoicesQuery request, CancellationToken ct)
    {
        var query = _db.Invoices
            .Include(i => i.Customer)
            .Include(i => i.Items)
            .Include(i => i.Users)
            .AsQueryable();

        if (request.UserId.HasValue)
        {
            query = query.Where(i => i.UserId == request.UserId);
        }

        if (request.CustomerId.HasValue)
        {
            query = query.Where(i => i.CustomerId == request.CustomerId);
        }

        if (!string.IsNullOrEmpty(request.Status))
        {
            query = query.Where(i => i.Status == request.Status);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(i => i.Date >= request.StartDate);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(i => i.Date <= request.EndDate);
        }

        return await query.ToListAsync(ct);
    }
}