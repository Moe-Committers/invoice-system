using invoice_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace invoice_system.Database.Configs;
public class QuotationConfig : IEntityTypeConfiguration<Quotations>
{
    public void Configure(EntityTypeBuilder<Quotations> builder)
    {
        builder.HasKey(q => q.Id);
        builder.Property(q => q.QuotationNumber).IsRequired().HasMaxLength(100);
        builder.Property(q => q.Date).IsRequired();
        builder.Property(q => q.Status).IsRequired().HasMaxLength(50);
        builder.Property(q => q.SubTotal).HasColumnType("decimal(10,2)");
        builder.Property(q => q.Deposit).HasColumnType("decimal(10,2)");
        builder.Property(q => q.DeliveryFee).HasColumnType("decimal(10,2)");
        builder.Property(q => q.FinalAmount).HasColumnType("decimal(10,2)");
        builder.HasOne(q => q.Customer).WithMany(c => c.Quotations).HasForeignKey(q => q.CustomerId);
        builder.HasMany(q => q.Items).WithOne(qi => qi.Quotation).HasForeignKey(qi => qi.QuotationId);
    }
}

