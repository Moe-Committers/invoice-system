using MediatR;

namespace invoice_system.Features.Quotation.Commands.Create;

public record CreateQuotationCommand : IRequest<Models.Quotations>
{
    public DateTime Date { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Deposit { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal FinalAmount { get; set; }
    public long CustomerId { get; set; }
    public long UserId { get; set; }
    public List<QuotationItemDto> Items { get; set; }
}

public class QuotationItemDto
{
    public string Description { get; set; }
    public string Unit { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}