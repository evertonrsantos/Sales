using Microsoft.EntityFrameworkCore;
using SalesApi.Domain.Contracts.Repository;
using SalesApi.Domain.Entities;
using SalesApi.Repository.Context;
using SalesApi.Repository.Repositories.Base;

namespace SalesApi.Repository.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(SalesDbContext context) : base(context)
    {
    }

    public async override Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products
            .Where(p => p.IsActive)
            .OrderBy(p => p.Title)
            .ToListAsync();
    }
}
