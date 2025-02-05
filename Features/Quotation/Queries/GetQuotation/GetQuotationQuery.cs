using System.Text.Json.Serialization;
using MediatR;

namespace invoice_system.Features.Quotation.Queries.GetQuotation;

public record GetQuotationQuery : IRequest<Models.Quotations>
{
    [JsonIgnore]
    public long QuotationId { get; set; }
}
