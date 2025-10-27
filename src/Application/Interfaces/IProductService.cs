using static ApiPrueba.src.Application.DTOs.ProductDtos;

namespace ApiPrueba.src.Application.Interfaces
{
    public interface IProductService
    {
        Task<(IReadOnlyList<ProductDto> Items, int Total)> GetPagedAsync(int page, int pageSize, string? q);
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(CreateProductRequest req);
        Task<bool> UpdateAsync(int id, UpdateProductRequest req);
        Task<bool> DeleteAsync(int id);
    }
}
