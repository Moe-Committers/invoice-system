namespace invoice_system.Models;

public class RolePermissions : BaseEntity{
    public long RoleId {get; set;}
    public virtual Roles Role {get; set;}
    public long PermissionId {get; set;}
    public virtual Permissions Permission {get; set;}
}