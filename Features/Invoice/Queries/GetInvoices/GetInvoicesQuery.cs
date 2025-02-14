using invoice_system.Utils.DTOs.invoiceDto;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;

namespace invoice_system.Features.Invoice.Queries.GetInvoices;

public record GetInvoicesQuery : IRequest<ApiResponse<List<InvDto>>>
{
    public string? UserName {get; set;}
    public string? Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Sort { get; set; } = "created";
    public bool IsAscending { get; set; } = false;
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}