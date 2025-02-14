using invoice_system.Database.Seeder.Constants;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Database.Seeder;

public class RolePermissionSeeder : IBaseSeeder
{
    private static readonly Dictionary<string, Func<string[]>> RolePermissionsMap = new()
    {
        { RoleStuffs.Roles.Admin, () => RoleStuffs.Permissions.All },

        { RoleStuffs.Roles.Manager, () => RoleStuffs.Permissions.All
            .Except(RoleStuffs.Permissions.DeleteOnly)
            .ToArray() },

        { RoleStuffs.Roles.Staff, () => RoleStuffs.Permissions.ViewOnly
            .Concat(RoleStuffs.Permissions.CreateOnly)
            .ToArray() }
    };

    public bool ShouldRun(Db db)
    {
        return !db.RolePermissions.Any();
    }

    public async Task Seeding(Db db)
    {
        var roles = await db.Roles.ToDictionaryAsync(r => r.Name, r => r);
        var permissions = await db.Permissions.ToDictionaryAsync(p => p.Name, p => p);

        var rolePermissions = new List<Models.RolePermissions>();

        foreach (var (roleName, getPermissions) in RolePermissionsMap)
        {
            var role = roles[roleName];
            foreach (var permissionName in getPermissions())
            {
                var permission = permissions[permissionName];
                rolePermissions.Add(new Models.RolePermissions
                {
                    RoleId = role.Id,
                    PermissionId = permission.Id,
                    CreatedAt = DateTime.UtcNow
                });
            }
        }

        await db.RolePermissions.AddRangeAsync(rolePermissions);
        await db.SaveChangesAsync();
    }
}