using System.Text.Json.Serialization;
using MediatR;

namespace invoice_system.Features.Invoice.Queries.GetInvoice;

public record GetInvoiceQuery : IRequest<Models.Invoices>
{
    [JsonIgnore]
    public long InvoiceId { get; set; }
}