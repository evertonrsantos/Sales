using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Entities;

namespace SalesApi.Repository.Configurations;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("sale_item");
        
        builder.HasKey(i => i.Id);
        
        builder.Property(i => i.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        builder.Property(i => i.SaleId)
            .HasColumnName("sale_id")
            .IsRequired();

        builder.Property(i => i.ProductId)
            .HasColumnName("product_id")
            .IsRequired();

        builder.Property(i => i.Quantity)
            .HasColumnName("quantity")
            .IsRequired();

        builder.Property(i => i.UnitPrice)
            .HasColumnName("unit_price")
            .IsRequired();
            
        builder.Property(i => i.ValueMonetaryTaxApplied)
            .HasColumnName("value_monetary_tax_applied")
            .IsRequired();
            
        builder.Property(i => i.Total)
            .HasColumnName("total")
            .IsRequired();

        builder.Property(i => i.IsCancelled)
            .HasColumnName("is_cancelled")
            .IsRequired();


        builder.HasOne(i => i.Sale)
            .WithMany(x => x.Items)
            .HasForeignKey(i => i.SaleId);

        builder.HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId);
    }
}
