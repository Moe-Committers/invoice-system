using invoice_system.Database;
using invoice_system.Utils.Exceptions;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Quotation.Commands.Convert;

public class ConvertQuotationCommandHandler : IRequestHandler<ConvertQuotationCommand, ApiResponse<Models.Invoices>>
{
    private readonly Db _db;

    public ConvertQuotationCommandHandler(Db db)
    {
        _db = db;
    }

    public async Task<ApiResponse<Models.Invoices>> Handle(ConvertQuotationCommand request, CancellationToken ct)
    {
        var quotation = await _db.Quotations
            .Include(q => q.Items)
            .FirstOrDefaultAsync(q => q.Id == request.QuotationId, ct);

        if (quotation == null)
        {
            throw new BadRequestExceptions("Quotation not found or has been deleted");
        }

        if (quotation.Status?.ToLower() == "converted")
        {
            throw new BadRequestExceptions("Quotation has already been converted to an invoice");
        }

        var lastInvoiceNumber = await _db.LastInvoiceNumbers
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(ct);

        var nextInvoiceNumber = lastInvoiceNumber == null ? 1 : lastInvoiceNumber.Value + 1;

        var invoice = new Models.Invoices
        {
            InvoiceNumber = $"INV{nextInvoiceNumber.ToString().PadLeft(5, '0')}",
            Date = DateTime.UtcNow,
            ExpirationDate = DateTime.UtcNow.AddDays(30), // Default 30 days, you might want to make this configurable
            Status = "pending",
            SubTotal = quotation.SubTotal,
            Deposit = quotation.Deposit,
            DeliveryFee = quotation.DeliveryFee,
            FinalPayment = quotation.FinalAmount,
            CustomerId = quotation.CustomerId,
            UserId = quotation.UserId,
            CreatedAt = DateTime.UtcNow,
            Items = quotation.Items.Select(item => new Models.InvoiceItems
            {
                Description = item.Description,
                Unit = item.Unit,
                Quantity = item.Quantity,
                Price = item.Price,
                Amount = item.Amount,
                CreatedAt = DateTime.UtcNow
            }).ToList()
        };

        if (lastInvoiceNumber == null)
        {
            await _db.LastInvoiceNumbers.AddAsync(new Models.LastInvoiceNumber
            {
                Value = nextInvoiceNumber,
                CreatedAt = DateTime.UtcNow
            }, ct);
        }
        else
        {
            lastInvoiceNumber.Value = nextInvoiceNumber;
            lastInvoiceNumber.UpdatedAt = DateTime.UtcNow;
            _db.LastInvoiceNumbers.Update(lastInvoiceNumber);
        }

        quotation.Status = "converted";
        quotation.UpdatedAt = DateTime.UtcNow;
        _db.Quotations.Update(quotation);

        await _db.Invoices.AddAsync(invoice, ct);
        await _db.SaveChangesAsync(ct);

        await _db.Entry(invoice)
            .Reference(i => i.Customer)
            .LoadAsync(ct);

        await _db.Entry(invoice)
            .Reference(i => i.Users)
            .LoadAsync(ct);

        return ResHelper.Success(
            invoice,
            message: "Quotation successfully converted to invoice"
        );
    }
}