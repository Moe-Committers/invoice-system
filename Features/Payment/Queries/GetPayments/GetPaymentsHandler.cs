using invoice_system.Database;
using invoice_system.Utils.Extensions;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;

namespace invoice_system.Features.Payment.Queries.GetPayments;

public class GetPaymentsHandler : IRequestHandler<GetPaymentsQuery, ApiResponse<List<CustomerWithInvoiceCount>>>
{
    private readonly Db _db;

    public GetPaymentsHandler(Db db)
    {
        _db = db;
    }

    public async Task<ApiResponse<List<CustomerWithInvoiceCount>>> Handle(GetPaymentsQuery request, CancellationToken ct)
    {

        var query = _db.Customers.AsQueryable();

        if (!string.IsNullOrEmpty(request.Search))
        {
            query = query.Where(c =>
                c.Name.Contains(request.Search) ||
                c.Id.ToString().Contains(request.Search)
            );
        }

        var customersWithCount = await query
            .Select(c => new CustomerWithInvoiceCount
            {
                Id = c.Id,
                Name = c.Name,
                InvoiceCount = _db.Invoices.Count()
            })
            .Where(c => c.InvoiceCount > 0)
            .UsePaginate(request.Page, request.PageSize, ct);

        return customersWithCount;
    }
}