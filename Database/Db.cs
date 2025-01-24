using Microsoft.EntityFrameworkCore;

namespace invoice_system.Database;

public class Db : DbContext{
    public Db(DbContextOptions<Db> options) : base(options){

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}