using System.ComponentModel.DataAnnotations;
using invoice_system.Utils.Enums;

namespace invoice_system.Models;

public class Users : BaseEntity{
    public Users(){
        Invoices = new HashSet<Invoices>();
        Customers = new HashSet<Customers>();
        Quotations = new HashSet<Quotations>();
        RefreshTokens = new HashSet<RefreshTokens>();
    }
    public string Name {get; set;}
    public string Email {get; set;}
    public int Age {get; set;}
    [MaxLength(9)]
    public int PhoneNumber {get; set;}
    public string Avatar {get; set;}
    public long RoleId {get; set;}
    public string Password {get; set;}
    public DateTime LastLogin {get; set;}
    public Status Status {get; set;}
    public virtual Roles Role {get; set;}
    public virtual ICollection<Invoices> Invoices {get; set;}
    public virtual ICollection<Customers> Customers {get; set;}
    public virtual ICollection<Quotations> Quotations {get; set;}
    public virtual ICollection<RefreshTokens> RefreshTokens {get; set;}
}