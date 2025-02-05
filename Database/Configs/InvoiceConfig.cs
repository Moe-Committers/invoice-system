using invoice_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace invoice_system.Database.Configs;
public class InvoiceConfig : IEntityTypeConfiguration<Invoices>
{
    public void Configure(EntityTypeBuilder<Invoices> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.InvoiceNumber).IsRequired().HasMaxLength(100);
        builder.Property(i => i.Date).IsRequired();
        builder.Property(i => i.Status).IsRequired().HasMaxLength(50);
        builder.Property(i => i.SubTotal).HasColumnType("decimal(10,2)");
        builder.Property(i => i.Deposit).HasColumnType("decimal(10,2)");
        builder.Property(i => i.DeliveryFee).HasColumnType("decimal(10,2)");
        builder.Property(i => i.FinalPayment).HasColumnType("decimal(10,2)");
        builder.HasOne(i => i.Customer).WithMany(c => c.Invoices).HasForeignKey(i => i.CustomerId);
        builder.HasMany(i => i.Items).WithOne(ii => ii.Invoice).HasForeignKey(ii => ii.InvoiceId);
    }
}

