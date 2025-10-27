using ApiPrueba.src.Domain.Entities;

namespace ApiPrueba.src.Domain.Repositories;

public interface IProductRepository
{
    Task<(IReadOnlyList<Product> Items, int Total)> GetPagedAsync(int page, int pageSize, string? q);
    Task<Product?> GetByIdAsync(int id);
    Task AddAsync(Product entity);
    void Update(Product entity);
    void Remove(Product entity);
}
