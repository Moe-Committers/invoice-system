using invoice_system.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace invoice_system.Database.Configs;
public class QuotationItemConfig : IEntityTypeConfiguration<QuotationItems>
{
    public void Configure(EntityTypeBuilder<QuotationItems> builder)
    {
        builder.HasKey(qi => qi.Id);
        builder.Property(qi => qi.Description).HasMaxLength(255);
        builder.Property(qi => qi.Unit).HasMaxLength(50);
        builder.Property(qi => qi.Quantity).HasColumnType("decimal(10,2)");
        builder.Property(qi => qi.Price).HasColumnType("decimal(10,2)");
        builder.Property(qi => qi.Amount).HasColumnType("decimal(10,2)");
        builder.HasOne(qi => qi.Quotation).WithMany(q => q.Items).HasForeignKey(qi => qi.QuotationId);
    }
}

