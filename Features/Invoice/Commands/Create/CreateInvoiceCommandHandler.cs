using invoice_system.Database;
using invoice_system.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Invoice.Commands.Create;

public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Models.Invoices>
{
    private readonly Db _db;

    public CreateInvoiceCommandHandler(Db db)
    {
        _db = db;
    }

    public async Task<Models.Invoices> Handle(CreateInvoiceCommand request, CancellationToken ct)
    {
        var customer = await _db.Customers
            .FirstOrDefaultAsync(c => c.Id == request.CustomerId, ct);

        if (customer == null)
        {
            throw new NotFoundExceptions("Customer not found");
        }

        var user = await _db.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, ct);

        if (user == null)
        {
            throw new NotFoundExceptions("User not found");
        }

        var lastInvoiceNumber = await _db.LastInvoiceNumbers
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(ct);

        var nextInvoiceNumber = lastInvoiceNumber == null ? 1 : lastInvoiceNumber.Value + 1;

        var invoice = new Models.Invoices
        {
            InvoiceNumber = $"INV{nextInvoiceNumber.ToString().PadLeft(5, '0')}",
            Date = request.Date,
            ExpirationDate = request.ExpirationDate,
            Status = request.Status,
            SubTotal = request.SubTotal,
            Deposit = request.Deposit,
            DeliveryFee = request.DeliveryFee,
            FinalPayment = request.FinalPayment,
            CustomerId = request.CustomerId,
            UserId = request.UserId,
            Items = request.Items.Select(item => new Models.InvoiceItems
            {
                Description = item.Description,
                Unit = item.Unit,
                Quantity = item.Quantity,
                Price = item.Price,
                Amount = item.Amount
            }).ToList()
        };

        if (lastInvoiceNumber == null)
        {
            await _db.LastInvoiceNumbers.AddAsync(new Models.LastInvoiceNumber { Value = nextInvoiceNumber }, ct);
        }
        else
        {
            lastInvoiceNumber.Value = nextInvoiceNumber;
            _db.LastInvoiceNumbers.Update(lastInvoiceNumber);
        }

        await _db.Invoices.AddAsync(invoice, ct);
        await _db.SaveChangesAsync(ct);

        return invoice;
    }
}