using System.Text.Json.Serialization;
using invoice_system.Utils.Helpers.ResHelpers;
using MediatR;

namespace invoice_system.Features.Quotation.Commands.Convert;

public record ConvertQuotationCommand : IRequest<ApiResponse<Models.Invoices>>
{
    [JsonIgnore]
    public long QuotationId { get; set; }
}