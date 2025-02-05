using invoice_system.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Invoice.Commands.Delete;

public class DeleteInvoiceCommandHandler : IRequestHandler<DeleteInvoiceCommand, bool>
{
    private readonly Db _db;

    public DeleteInvoiceCommandHandler(Db db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteInvoiceCommand request, CancellationToken ct)
    {
        var invoice = await _db.Invoices
            .Include(i => i.Items)
            .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, ct);

        if (invoice == null)
        {
            return false;
        }

        _db.InvoiceItems.RemoveRange(invoice.Items);
        _db.Invoices.Remove(invoice);
        await _db.SaveChangesAsync(ct);

        return true;
    }
}