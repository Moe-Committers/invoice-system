namespace invoice_system.Models;

public class QuotationItems : BaseEntity
{
    public string Description { get; set; }
    public string Unit { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
    public long QuotationId { get; set; }
    public virtual Quotations Quotation { get; set; }
}