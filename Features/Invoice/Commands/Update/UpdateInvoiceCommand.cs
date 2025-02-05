using System.Text.Json.Serialization;
using invoice_system.Features.Invoice.Commands.Create;
using MediatR;

namespace invoice_system.Features.Invoice.Commands.Update;

public record UpdateInvoiceCommand : IRequest<Models.Invoices>
{
    [JsonIgnore]
    public long InvoiceId { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Deposit { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal FinalPayment { get; set; }
    public List<InvoiceItemDto> Items { get; set; }
}