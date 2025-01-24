namespace invoice_system.Models;

public class Permissions : BaseEntity{
    public Permissions(){
        RolePermissions = new HashSet<RolePermissions>();
    }
    public string Name {get; set;}
    public virtual ICollection<RolePermissions> RolePermissions {get; set;}
}