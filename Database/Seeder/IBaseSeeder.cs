namespace invoice_system.Database.Seeder;

public interface IBaseSeeder {
    bool ShouldRun(Db db);
    Task Seeding(Db db);
}