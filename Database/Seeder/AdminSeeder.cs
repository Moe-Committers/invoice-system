using invoice_system.Database.Seeder.Constants;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Database.Seeder;

public class AdminSeeder : IBaseSeeder
{
    public bool ShouldRun(Db db)
    {
        return !db.Users.Any(u => u.Email == RoleStuffs.DefaultAdmin.Email);
    }

    public async Task Seeding(Db db)
    {

        var adminRole = await db.Roles.FirstOrDefaultAsync(r =>
            r.Name == RoleStuffs.Roles.Admin);

        if (adminRole == null)
        {
            throw new Exception($"Role '{RoleStuffs.Roles.Admin}' not found. Please run RoleSeeder first.");
        }

        var adminUser = new Models.Users
        {
            Name = RoleStuffs.DefaultAdmin.Name,
            Email = RoleStuffs.DefaultAdmin.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(RoleStuffs.DefaultAdmin.Password),
            Age = RoleStuffs.DefaultAdmin.Age,
            PhoneNumber = RoleStuffs.DefaultAdmin.PhoneNumber,
            RoleId = adminRole.Id,
            Status = RoleStuffs.DefaultAdmin.status,
            CreatedAt = DateTime.UtcNow
        };

        await db.Users.AddAsync(adminUser);
        await db.SaveChangesAsync();
    }
}
