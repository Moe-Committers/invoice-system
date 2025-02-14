using invoice_system.Database;
using invoice_system.Utils.DTOs.invoiceDto;
using invoice_system.Utils.Extensions;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Invoice.Queries.GetInvoices;

public class GetInvoicesQueryHandler : IRequestHandler<GetInvoicesQuery, ApiResponse<List<InvDto>>>
{
    private readonly Db _db;

    public GetInvoicesQueryHandler(Db db)
    {
        _db = db;
    }

    public async Task<ApiResponse<List<InvDto>>> Handle(GetInvoicesQuery request, CancellationToken ct)
    {
        var query = _db.Invoices
            .Include(i => i.Customer)
            .Include(i => i.Users)
            .Select(i => new InvDto
            {
                Id = i.Id,
                InvoiceNumber = i.InvoiceNumber,
                Status = i.Status,
                CustomerName = i.Customer.Name,
                CteatorName = i.Users.Name,
                FinalPayment = i.FinalPayment,
                Date = i.Date
            })
            .AsQueryable();

        if (!string.IsNullOrEmpty(request.UserName))
        {
            query = query.Where(i => i.CustomerName.Contains(request.UserName));
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

        query = request.Sort?.ToLower() switch
        {
            "invno" => request.IsAscending
                ? query.OrderBy(c => c.InvoiceNumber)
                : query.OrderByDescending(c => c.InvoiceNumber),
            "created" => request.IsAscending
                ? query.OrderBy(c => c.Date)
                : query.OrderByDescending(c => c.Date),
            _ => query.OrderByDescending(c => c.Date)
        };

        return await query.UsePaginate(request.Page, request.PageSize, ct);
    }
}