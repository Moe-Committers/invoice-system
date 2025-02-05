using invoice_system.Database.Configs;
using invoice_system.Models;
using Microsoft.EntityFrameworkCore;

namespace invoice_system.Database;

public class Db : DbContext
{
    public Db(DbContextOptions<Db> options) : base(options)
    {

    }
    public DbSet<Users> Users {get; set;}
    public DbSet<Customers> Customers { get; set; }
    public DbSet<Invoices> Invoices { get; set; }
    public DbSet<InvoiceItems> InvoiceItems { get; set; }
    public DbSet<Permissions> Permissions { get; set; }
    public DbSet<QuotationItems> QuotationItems { get; set; }
    public DbSet<Quotations> Quotations { get; set; }
    public DbSet<RefreshTokens> RefreshTokens { get; set; }
    public DbSet<RolePermissions> RolePermissions { get; set; }
    public DbSet<LastInvoiceNumber> LastInvoiceNumbers { get; set; }
    public DbSet<LastQuotationNumber> LastQuotationNumbers { get; set; }
    public DbSet<Roles> Roles {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfig());
        modelBuilder.ApplyConfiguration(new CustomerConfig());
        modelBuilder.ApplyConfiguration(new InvoiceItemConfig());
        modelBuilder.ApplyConfiguration(new InvoiceConfig());
        modelBuilder.ApplyConfiguration(new PermissionConfig());
        modelBuilder.ApplyConfiguration(new QuotationItemConfig());
        modelBuilder.ApplyConfiguration(new QuotationConfig());
        modelBuilder.ApplyConfiguration(new RefreshTokenConfig());
        modelBuilder.ApplyConfiguration(new RolePermissionConfig());
        modelBuilder.ApplyConfiguration(new RoleConfig());
    }
}