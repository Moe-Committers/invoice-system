namespace invoice_system.Models;

public class Customers : BaseEntity{
    public Customers() {
        Invoices = new HashSet<Invoices>();
        Quotations = new HashSet<Quotations>();
    }
    public string Name { get; set; }
    public string Attention { get; set; }
    public string Tel { get; set; }
    public long UserId {get; set;}
    public virtual Users Users {get; set;}
    public virtual ICollection<Invoices> Invoices { get; set; }
    public virtual ICollection<Quotations> Quotations { get; set; }
}