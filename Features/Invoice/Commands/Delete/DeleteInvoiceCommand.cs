using System.Text.Json.Serialization;
using MediatR;

namespace invoice_system.Features.Invoice.Commands.Delete;

public record DeleteInvoiceCommand : IRequest<bool>
{
    [JsonIgnore]
    public long InvoiceId { get; set; }
}
