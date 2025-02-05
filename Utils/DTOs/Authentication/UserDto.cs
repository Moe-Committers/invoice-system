using invoice_system.Models;
using invoice_system.Utils.Enums;

namespace invoice_system.Utils.DTOs.Authentication;

public class UserDto{
    public long Id {get; set;}
    public string Name {get; set;}
    public string Email {get; set;}
    public int Age {get; set;}
    public Roles role {get; set;}
    public Status status {get; set;}
    public DateTime CreatedAt {get; set;}
    public string Profile {get; set;}
}