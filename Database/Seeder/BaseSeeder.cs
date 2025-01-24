namespace invoice_system.Database.Seeder;

public class BaseSeeder {
    private static readonly IBaseSeeder[] Seeders = new IBaseSeeder[] {

    };

    public static async Task SeedingDb(IServiceProvider service){
        var scoped = service.CreateScope();
        var db = scoped.ServiceProvider.GetRequiredService<Db>();
        foreach(var seeder in Seeders){
            if(seeder.ShouldRun(db)){
                await seeder.Seeding(db);
            }
        }
    }
}