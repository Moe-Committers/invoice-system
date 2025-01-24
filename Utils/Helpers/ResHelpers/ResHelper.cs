namespace invoice_system.Utils.Helpers.ResHelpers;

public class ResHelper {
    public static ApiResponse<T> Success<T>(T? data = null , PaginateResponse? paginate = null , string message = "success") where T : class{
        return new ApiResponse<T>{
            success = true, 
            message = message,
            data = data,
            paginate = paginate
        };
    }

    public static ApiResponse<T> Errors<T>(string message = "errors!" , Dictionary<string ,string> errors = null) where T : class{
        return new ApiResponse<T>{
            success = false,
            message = message,
            errors = errors ?? new Dictionary<string, string>()
        };
    }
}