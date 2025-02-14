namespace invoice_system.Utils.DTOs.UserMDto;

public class RolePermissionsResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<string> Permissions { get; set; }
}
