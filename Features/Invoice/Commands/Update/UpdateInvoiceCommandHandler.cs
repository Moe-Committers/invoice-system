using invoice_system.Database;
using invoice_system.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Invoice.Commands.Update;

public class UpdateInvoiceCommandHandler : IRequestHandler<UpdateInvoiceCommand, Models.Invoices>
{
    private readonly Db _db;

    public UpdateInvoiceCommandHandler(Db db)
    {
        _db = db;
    }

    public async Task<Models.Invoices> Handle(UpdateInvoiceCommand request, CancellationToken ct)
    {
        var invoice = await _db.Invoices
            .Include(i => i.Items)
            .FirstOrDefaultAsync(i => i.Id == request.InvoiceId, ct);

        if (invoice == null)
        {
            throw new NotFoundExceptions("Invoice not found");
        }

        invoice.ExpirationDate = request.ExpirationDate;
        invoice.Status = request.Status;
        invoice.SubTotal = request.SubTotal;
        invoice.Deposit = request.Deposit;
        invoice.DeliveryFee = request.DeliveryFee;
        invoice.FinalPayment = request.FinalPayment;
        invoice.UpdatedAt = DateTime.UtcNow;

        _db.InvoiceItems.RemoveRange(invoice.Items);

        invoice.Items = request.Items.Select(item => new Models.InvoiceItems
        {
            Description = item.Description,
            Unit = item.Unit,
            Quantity = item.Quantity,
            Price = item.Price,
            Amount = item.Amount,
            InvoiceId = invoice.Id
        }).ToList();

        _db.Invoices.Update(invoice);
        await _db.SaveChangesAsync(ct);

        return invoice;
    }
}
