namespace invoice_system.Models;

public class Quotations : BaseEntity{
    public Quotations(){
        Items = new HashSet<QuotationItems>();
    }
    public string QuotationNumber { get; set; }
    public DateTime Date { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Deposit { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal FinalAmount { get; set; }
    public long CustomerId { get; set; }
    public long UserId {get; set;}
    public virtual Users Users {get; set;}
    public virtual Customers Customer { get; set; }
    public virtual ICollection<QuotationItems> Items { get; set; }
}