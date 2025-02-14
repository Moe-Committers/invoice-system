using invoice_system.Database.Seeder.Constants;

namespace invoice_system.Database.Seeder;

public class RoleSeeder : IBaseSeeder
{
    public bool ShouldRun(Db db)
    {
        return !db.Roles.Any();
    }

    public async Task Seeding(Db db)
    {
        var roles = RoleStuffs.Roles.All
            .Select(name => new Models.Roles
            {
                Name = name,
                CreatedAt = DateTime.UtcNow
            });

        db.Roles.AddRange(roles);
        await db.SaveChangesAsync();
    }
}