using invoice_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace invoice_system.Database.Configs;
public class InvoiceItemConfig : IEntityTypeConfiguration<InvoiceItems>
{
    public void Configure(EntityTypeBuilder<InvoiceItems> builder)
    {
        builder.HasKey(ii => ii.Id);
        builder.Property(ii => ii.Description).HasMaxLength(255);
        builder.Property(ii => ii.Unit).HasMaxLength(50);
        builder.Property(ii => ii.Quantity).HasColumnType("decimal(10,2)");
        builder.Property(ii => ii.Price).HasColumnType("decimal(10,2)");
        builder.Property(ii => ii.Amount).HasColumnType("decimal(10,2)");
        builder.HasOne(ii => ii.Invoice).WithMany(i => i.Items).HasForeignKey(ii => ii.InvoiceId);
    }
}

