namespace invoice_system.Models;

public class Invoices : BaseEntity
{
    public Invoices(){
        Items = new HashSet<InvoiceItems>();
    }
    public string InvoiceNumber { get; set; }
    public DateTime Date { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Deposit { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal FinalPayment { get; set; }
    public long CustomerId { get; set; }
    public long UserId {get; set;}
    public virtual Users Users {get; set;}
    public virtual Customers Customer { get; set; }
    public virtual ICollection<InvoiceItems> Items { get; set; }
}