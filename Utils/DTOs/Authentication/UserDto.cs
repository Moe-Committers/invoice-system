using invoice_system.Utils.Enums;

namespace invoice_system.Utils.DTOs.Authentication;

public class UserDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public int Age { get; set; }
    public string Role { get; set; }
    public Status Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Avatar { get; set; }
}