using invoice_system.Database;
using invoice_system.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Invoice.Queries.GetInvoice;

public class GetInvoiceQueryHandler : IRequestHandler<GetInvoiceQuery, Models.Invoices>
{
    private readonly Db _db;

    public GetInvoiceQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<Models.Invoices> Handle(GetInvoiceQuery request, CancellationToken ct)
    {
        var invoice = await _db.Invoices
            .Include(i => i.Customer)
            .Include(i => i.Items)
            .Include(i => i.Users)
            .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, ct);

        if (invoice == null)
        {
            throw new NotFoundExceptions("Invoice not found");
        }

        return invoice;
    }
}