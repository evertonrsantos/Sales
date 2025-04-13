using Microsoft.EntityFrameworkCore;
using SalesApi.Domain.Contracts.Repository;
using SalesApi.Domain.Entities;
using SalesApi.Repository.Context;
using SalesApi.Repository.Repositories.Base;

namespace SalesApi.Repository.Repositories;

public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
{
    public SaleRepository(SalesDbContext context) : base(context) { }

    public async override Task<IEnumerable<Sale>> GetAllAsync()
    {
        return await _context.Sales
            .Include(s => s.Items)
            .OrderByDescending(s => s.Date)
            .ToListAsync();
    }
}
