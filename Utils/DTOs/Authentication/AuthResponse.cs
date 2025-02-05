namespace invoice_system.Utils.DTOs.Authentication;

public class AuthResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime ExpiresIn { get; set; }
    public required UserDto User { get; set; }
}