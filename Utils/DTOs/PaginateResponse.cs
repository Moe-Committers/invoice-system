namespace invoice_system.Utils.DTOs;

public class PaginateResponse{
    public int totalcounts {get; set;}
    public int page {get; set;}
    public int pagesizes {get; set;}
    public int totalpages {get; set;}
}