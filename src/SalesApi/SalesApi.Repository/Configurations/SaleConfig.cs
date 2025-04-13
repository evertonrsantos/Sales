using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Entities;

namespace SalesApi.Repository.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("sale");
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();

        builder.Property(s => s.SaleNumber)
            .HasColumnName("sale_number")
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(s => s.Date)
            .HasColumnName("date")
            .IsRequired();

        builder.Property(s => s.TotalAmount)
            .HasColumnName("total_amount")
            .HasColumnType("decimal(18,2)");

        builder.Property(s => s.CustomerId)
            .HasColumnName("customer_id")
            .IsRequired();

        builder.Property(s => s.BranchId)
            .HasColumnName("branch_id")
            .IsRequired();

        builder.Property(s => s.TotalAmount)
            .HasColumnName("total_amount")
            .IsRequired();

        builder.Property(s => s.IsCancelled)
            .HasColumnName("is_cancelled")
            .IsRequired();
    }
}
