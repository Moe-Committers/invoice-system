using invoice_system.Database;
using invoice_system.Utils.DTOs.invoiceDto;
using invoice_system.Utils.Extensions;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Features.Payment.Queries.GetCustomerInvoices;

public class GetCustomerInvoicesHandler : IRequestHandler<GetCustomerInvoices, ApiResponse<List<InvDto>>>
{
    private readonly Db _db;

    public GetCustomerInvoicesHandler(Db db)
    {
        _db = db;
    }

    public async Task<ApiResponse<List<InvDto>>> Handle(GetCustomerInvoices request, CancellationToken ct)
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
            }).AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(i =>
                i.Status.Contains(request.Search) ||
                i.InvoiceNumber.Contains(request.Search)
            );
        }

        if (request.StartDate.HasValue && request.EndDate.HasValue)
        {
            query = query.Where(i =>
                i.Date >= request.StartDate &&
                i.Date <= request.EndDate
            );
        }

        query = query.OrderByDescending(i => i.Date);

        var result = await query.UsePaginate(request.Page, request.PageSize, ct);
        return result;
    }
}