namespace invoice_system.Utils.DTOs.invoiceDto;

public class InvDto {
    public long Id {get; set;}
    public string InvoiceNumber {get; set;}
    public string Status {get; set;}
    public string CustomerName {get; set;}
    public string CteatorName {get; set;}
    public decimal FinalPayment {get; set;}
    public DateTime Date {get; set;}
}