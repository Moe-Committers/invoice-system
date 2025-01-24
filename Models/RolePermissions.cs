namespace invoice_system.Models;

public class RolePermissions : BaseEntity{
    public virtual Roles Role {get; set;}
    public virtual Permissions Permission {get; set;}
}