using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;

namespace invoice_system.Features.Payment.Queries.GetPayments;

public record GetPaymentsQuery : IRequest<ApiResponse<List<CustomerWithInvoiceCount>>>
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Search { get; set; }
}

public class CustomerWithInvoiceCount
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int InvoiceCount { get; set; }
}
