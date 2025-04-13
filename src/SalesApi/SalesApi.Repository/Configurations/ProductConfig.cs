using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesApi.Domain.Entities;

namespace SalesApi.Repository.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("product");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("gen_random_uuid()")
            .ValueGeneratedOnAdd();
            
        builder.Property(p => p.Title)
            .HasColumnName("title")
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasMaxLength(500);
            
        builder.Property(p => p.Category)
            .HasColumnName("category")
            .HasMaxLength(100);
            
        builder.Property(p => p.Image)
            .HasColumnName("image")
            .HasMaxLength(500);
            
        builder.Property(p => p.Price)
            .HasColumnName("price")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(p => p.IsActive)
            .HasColumnName("is_active")
            .IsRequired();
    }
}
