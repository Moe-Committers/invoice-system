using invoice_system.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Quotation.Commands.Delete;

public class DeleteQuotationCommandHandler : IRequestHandler<DeleteQuotationCommand, bool>
{
    private readonly Db _db;

    public DeleteQuotationCommandHandler(Db db)
    {
        _db = db;
    }

    public async Task<bool> Handle(DeleteQuotationCommand request, CancellationToken ct)
    {
        var quotation = await _db.Quotations
            .Include(q => q.Items)
            .FirstOrDefaultAsync(q => q.Id == request.QuotationId, ct);

        if (quotation == null)
        {
            return false;
        }

        _db.QuotationItems.RemoveRange(quotation.Items);
        _db.Quotations.Remove(quotation);
        await _db.SaveChangesAsync(ct);

        return true;
    }
}