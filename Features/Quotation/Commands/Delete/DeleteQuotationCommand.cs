using System.Text.Json.Serialization;
using MediatR;

namespace invoice_system.Features.Quotation.Commands.Delete;

public record DeleteQuotationCommand : IRequest<bool>
{
    [JsonIgnore]
    public long QuotationId { get; set; }
}