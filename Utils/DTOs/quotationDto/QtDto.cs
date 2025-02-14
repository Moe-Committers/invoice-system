namespace invoice_system.Utils.DTOs.QuotationDto;

public class QtDto {
    public long Id {get; set;}
    public string QuotationNumber {get; set;}
    public string Status {get; set;}
    public string CustomerName {get; set;}
    public string CreatorName {get; set;}
    public decimal FinalPayment {get; set;}
    public DateTime Date {get; set;}
}