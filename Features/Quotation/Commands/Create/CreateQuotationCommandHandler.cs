using invoice_system.Database;
using invoice_system.Utils.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Quotation.Commands.Create;

public class CreateQuotationCommandHandler : IRequestHandler<CreateQuotationCommand, Models.Quotations>
{
    private readonly Db _db;

    public CreateQuotationCommandHandler(Db db)
    {
        _db = db;
    }

    public async Task<Models.Quotations> Handle(CreateQuotationCommand request, CancellationToken ct)
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

        var lastQuotationNumber = await _db.LastQuotationNumbers
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync(ct);

        var nextQuotationNumber = lastQuotationNumber == null ? 1 : lastQuotationNumber.Value + 1;

        var quotation = new Models.Quotations
        {
            QuotationNumber = $"QT{nextQuotationNumber.ToString().PadLeft(5, '0')}",
            Date = request.Date,
            ExpirationDate = request.ExpirationDate,
            Status = request.Status,
            SubTotal = request.SubTotal,
            Deposit = request.Deposit,
            DeliveryFee = request.DeliveryFee,
            FinalAmount = request.FinalAmount,
            CustomerId = request.CustomerId,
            UserId = request.UserId,
            Items = request.Items.Select(item => new Models.QuotationItems
            {
                Description = item.Description,
                Unit = item.Unit,
                Quantity = item.Quantity,
                Price = item.Price,
                Amount = item.Amount
            }).ToList()
        };

        if (lastQuotationNumber == null)
        {
            await _db.LastQuotationNumbers.AddAsync(new Models.LastQuotationNumber { Value = nextQuotationNumber }, ct);
        }
        else
        {
            lastQuotationNumber.Value = nextQuotationNumber;
            _db.LastQuotationNumbers.Update(lastQuotationNumber);
        }

        await _db.Quotations.AddAsync(quotation, ct);
        await _db.SaveChangesAsync(ct);

        return quotation;
    }
}