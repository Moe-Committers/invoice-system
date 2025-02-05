using MediatR;

namespace invoice_system.Features.Invoice.Commands.Create;

public record CreateInvoiceCommand : IRequest<Models.Invoices>
{
    public DateTime Date { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Deposit { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal FinalPayment { get; set; }
    public long CustomerId { get; set; }
    public long UserId { get; set; }
    public List<InvoiceItemDto> Items { get; set; }
}

public class InvoiceItemDto
{
    public string Description { get; set; }
    public string Unit { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}
