using Microsoft.EntityFrameworkCore;
using SalesApi.Domain.Entities;
using System.Reflection;

namespace SalesApi.Repository.Context;

public class SalesDbContext : DbContext
{
    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Sale> Sales { get; set; } = null!;
    public DbSet<SaleItem> SaleItems { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
             .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Model.GetEntityTypes().ToList().ForEach(entity =>
        {
            entity.GetProperties()
                  .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?))
                  .ToList()
                  .ForEach(property =>
                  {
                      property.SetColumnType("decimal(18,2)");
                  });

            entity.GetProperties()
                  .Where(p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?))
                  .ToList()
                  .ForEach(property =>
                  {
                      property.SetColumnType("timestamp");

                      if (property.Name.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
                          property.SetDefaultValueSql("CURRENT_TIMESTAMP");
                  });
        });

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
