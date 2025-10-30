namespace ApiPrueba.src.Application.DTOs;

public class ProductDtos
{
    public record ProductDto(int Id, string Name, decimal Price, int Stock, DateTime CreatedAt);
    public record CreateProductRequest(string Name, decimal Price, int Stock);
    public record UpdateProductRequest(string Name, decimal Price, int Stock);
}
