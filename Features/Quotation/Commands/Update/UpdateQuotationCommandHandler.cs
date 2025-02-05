using invoice_system.Database;
using invoice_system.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Quotation.Commands.Update;

public class UpdateQuotationCommandHandler : IRequestHandler<UpdateQuotationCommand, Models.Quotations>
{
    private readonly Db _db;

    public UpdateQuotationCommandHandler(Db db)
    {
        _db = db;
    }

    public async Task<Models.Quotations> Handle(UpdateQuotationCommand request, CancellationToken ct)
    {
        var quotation = await _db.Quotations
            .Include(q => q.Items)
            .FirstOrDefaultAsync(q => q.Id == request.QuotationId, ct);

        if (quotation == null)
        {
            throw new NotFoundExceptions("Quotation not found");
        }

        quotation.ExpirationDate = request.ExpirationDate;
        quotation.Status = request.Status;
        quotation.SubTotal = request.SubTotal;
        quotation.Deposit = request.Deposit;
        quotation.DeliveryFee = request.DeliveryFee;
        quotation.FinalAmount = request.FinalAmount;
        quotation.UpdatedAt = DateTime.UtcNow;

        _db.QuotationItems.RemoveRange(quotation.Items);

        quotation.Items = request.Items.Select(item => new Models.QuotationItems
        {
            Description = item.Description,
            Unit = item.Unit,
            Quantity = item.Quantity,
            Price = item.Price,
            Amount = item.Amount,
            QuotationId = quotation.Id
        }).ToList();

        _db.Quotations.Update(quotation);
        await _db.SaveChangesAsync(ct);

        return quotation;
    }
}