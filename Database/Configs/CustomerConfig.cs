using invoice_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace invoice_system.Database.Configs;
public class CustomerConfig : IEntityTypeConfiguration<Customers>
{
    public void Configure(EntityTypeBuilder<Customers> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Tel).HasMaxLength(20);
        builder.Property(c => c.Attention).HasMaxLength(100);
        builder.HasMany(c => c.Invoices).WithOne(i => i.Customer).HasForeignKey(i => i.CustomerId);
        builder.HasMany(c => c.Quotations).WithOne(q => q.Customer).HasForeignKey(q => q.CustomerId);
    }
}
