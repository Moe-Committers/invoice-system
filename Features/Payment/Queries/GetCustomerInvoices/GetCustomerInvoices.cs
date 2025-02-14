using System.Text.Json.Serialization;
using invoice_system.Utils.DTOs.invoiceDto;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;

namespace invoice_system.Features.Payment.Queries.GetCustomerInvoices;

public record GetCustomerInvoices : IRequest<ApiResponse<List<InvDto>>>
{
    [JsonIgnore]
    public long CustomerId { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Search { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}