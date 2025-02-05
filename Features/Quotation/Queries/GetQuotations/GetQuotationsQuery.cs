using MediatR;

namespace invoice_system.Features.Quotation.Queries.GetQuotations;

public record GetQuotationsQuery : IRequest<List<Models.Quotations>>
{
    public long? UserId { get; set; }
    public long? CustomerId { get; set; }
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}