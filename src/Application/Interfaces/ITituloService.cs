using ApiPrueba.src.Domain.Entities;

namespace ApiPrueba.src.Application.Interfaces;

public interface ITituloService : IService<Titulo, int>
{
    Task<IEnumerable<Titulo>> BuscarPorGeneroAsync( string genero);
    Task<(IEnumerable<Titulo> Items, int Total)> BuscarPaginadoAsync(string? nombre, string? tipo, string? genero, int? año,int page, int pageSize, string? sortBy, bool desc);
   
}
