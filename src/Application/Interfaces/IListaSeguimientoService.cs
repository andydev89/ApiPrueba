using ApiPrueba.src.Domain.Entities;

namespace ApiPrueba.src.Application.Interfaces;

public interface IListaSeguimientoService : IService<ListaSeguimiento, int>
{
    Task<IEnumerable<ListaSeguimiento>> GetByUsuarioAsync(int usuarioId);
    Task<bool> AgregarTituloAsync(int listaId, int tituloId);
    Task<bool> EliminarTituloAsync(int listaId, int tituloId);
}
