using ApiPrueba.src.Domain.Entities;
using ApiPrueba.src.Domain.Repositories;
using ApiPrueba.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ApiPrueba.src.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _db;
    public ProductRepository(AppDbContext db) => _db = db;

    public async Task<(IReadOnlyList<Product> Items, int Total)> GetPagedAsync(int page, int pageSize, string? q)
    {
        var query = _db.Products.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q))
            query = query.Where(p => p.Name.Contains(q));
        var total = await query.CountAsync();
        var items = await query.OrderByDescending(p => p.CreatedAt)
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToListAsync();
        return (items, total);
    }

    public Task<Product?> GetByIdAsync(int id) => _db.Products.FirstOrDefaultAsync(x => x.Id == id);
    public async Task AddAsync(Product e) => await _db.Products.AddAsync(e);
    public void Update(Product e) => _db.Products.Update(e);
    public void Remove(Product e) => _db.Products.Remove(e);
}
