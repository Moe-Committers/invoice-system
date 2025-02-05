using System.Text.Json.Serialization;
using invoice_system.Features.Quotation.Commands.Create;
using MediatR;

namespace invoice_system.Features.Quotation.Commands.Update;

public record UpdateQuotationCommand : IRequest<Models.Quotations>
{
    [JsonIgnore]
    public long QuotationId { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Deposit { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal FinalAmount { get; set; }
    public List<QuotationItemDto> Items { get; set; }
}
