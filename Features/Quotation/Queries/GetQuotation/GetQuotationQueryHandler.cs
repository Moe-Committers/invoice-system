using invoice_system.Database;
using invoice_system.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Quotation.Queries.GetQuotation;

public class GetQuotationQueryHandler : IRequestHandler<GetQuotationQuery, Models.Quotations>
{
    private readonly Db _db;

    public GetQuotationQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<Models.Quotations> Handle(GetQuotationQuery request, CancellationToken ct)
    {
        var quotation = await _db.Quotations
            .Include(q => q.Customer)
            .Include(q => q.Items)
            .Include(q => q.Users)
            .FirstOrDefaultAsync(q => q.Id == request.QuotationId, ct);

        if (quotation == null)
        {
            throw new NotFoundExceptions("Quotation not found");
        }

        return quotation;
    }
}