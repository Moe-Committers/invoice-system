using MediatR;

namespace invoice_system.Features.Invoice.Queries.GetInvoices;

public record GetInvoicesQuery : IRequest<List<Models.Invoices>>
{
    public long? UserId { get; set; }
    public long? CustomerId { get; set; }
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}