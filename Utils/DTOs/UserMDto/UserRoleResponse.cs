namespace invoice_system.Utils.DTOs.UserMDto;

public class UserRoleResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Profile { get; set; }
    public string Role { get; set; }
    public List<string> Permissions { get; set; }
}