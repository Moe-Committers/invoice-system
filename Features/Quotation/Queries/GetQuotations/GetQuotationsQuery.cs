using invoice_system.Utils.DTOs.QuotationDto;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;

namespace invoice_system.Features.Quotation.Queries.GetQuotations;

public record GetQuotationsQuery : IRequest<ApiResponse<List<QtDto>>>
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