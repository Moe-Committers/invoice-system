using invoice_system.Database.Seeder.Constants;

namespace invoice_system.Database.Seeder;

public class PermissionSeeder : IBaseSeeder
{
    public bool ShouldRun(Db db)
    {
        return !db.Permissions.Any();
    }

    public async Task Seeding(Db db)
    {
        var permissions = RoleStuffs.Permissions.All
            .Select(name => new Models.Permissions
            {
                Name = name,
                CreatedAt = DateTime.UtcNow
            });

        db.Permissions.AddRange(permissions);
        await db.SaveChangesAsync();
    }
}