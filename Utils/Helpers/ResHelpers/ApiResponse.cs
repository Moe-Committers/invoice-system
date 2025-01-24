namespace invoice_system.Utils.Helpers.ResHelpers;

public class ApiResponse<T>{
    public bool? success {get; set;} = false;
    public string? message {get; set;}
    public T data {get; set;}
    public PaginateResponse paginate {get; set;}
    public Dictionary<string , string> errors {get; set;}    
}