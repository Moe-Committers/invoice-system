namespace invoice_system.Models;

public class InvoiceItems : BaseEntity
{
    public string Description { get; set; }
    public string Unit { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
    public long InvoiceId { get; set; }
    public virtual Invoices Invoice { get; set; }
}