namespace invoice_system.Models;

public class Roles : BaseEntity{
    public Roles(){
        Users = new HashSet<Users>();
        RolePermissions = new HashSet<RolePermissions>();
    }
    public string Name {get; set;}
    public virtual ICollection<Users> Users {get; set;}
    public virtual ICollection<RolePermissions> RolePermissions {get; set;}
}