using ApiPrueba.src.Application.Interfaces;
using ApiPrueba.src.Domain.Entities;
using ApiPrueba.src.Domain.Repositories;
using static ApiPrueba.src.Application.DTOs.ProductDtos;

namespace ApiPrueba.src.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IUnitOfWork _uow;
        public ProductService(IProductRepository repo, IUnitOfWork uow)
        {
            _repo = repo; 
            _uow = uow;
        }

        public async Task<(IReadOnlyList<ProductDto> Items, int Total)> GetPagedAsync(int page, int pageSize, string? q)
        {
            var (items, total) = await _repo.GetPagedAsync(page, pageSize, q);
            return (items.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock, p.CreatedAt)).ToList(), total);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var p = await _repo.GetByIdAsync(id);
            return p is null ? null : new ProductDto(p.Id, p.Name, p.Price, p.Stock, p.CreatedAt);
        }

        public async Task<ProductDto> CreateAsync(CreateProductRequest req)
        {
            if (string.IsNullOrWhiteSpace(req.Name) || req.Price < 0 || req.Stock < 0)
                throw new ArgumentException("Datos inválidos.");
            var entity = new Product { Name = req.Name.Trim(), Price = req.Price, Stock = req.Stock };
            await _repo.AddAsync(entity);
            await _uow.SaveChangesAsync();
            return new ProductDto(entity.Id, entity.Name, entity.Price, entity.Stock, entity.CreatedAt);
        }

        public async Task<bool> UpdateAsync(int id, UpdateProductRequest req)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return false;
            entity.Name = req.Name.Trim();
            entity.Price = req.Price;
            entity.Stock = req.Stock;
            _repo.Update(entity);
            await _uow.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return false;
            _repo.Remove(entity);
            await _uow.SaveChangesAsync();
            return true;
        }
    }
}
