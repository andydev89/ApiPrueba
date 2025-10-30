using ApiPrueba.src.Application.Interfaces;
using ApiPrueba.src.Domain.Entities;
using ApiPrueba.src.Domain.Exeptions;
using ApiPrueba.src.Domain.Repositories;
using ApiPrueba.src.Infrastructure.Repositories;

namespace ApiPrueba.src.Application.Services;

public class ListaSeguimientoService : Service<ListaSeguimiento, int>, IListaSeguimientoService
{
    private readonly IRepository<ListaSeguimiento, int> _listasRepo;
    private readonly IRepository<ElementoLista, int> _elementosRepo;
    private readonly ILogger<ListaSeguimientoService> _logger;
   private readonly IRepository<Titulo, int> _tituloRepo;

    public ListaSeguimientoService(IRepository<ListaSeguimiento, int> listasRepo, IRepository<ElementoLista, int> elementosRepo, ILogger<ListaSeguimientoService> logger, IRepository<Titulo, int> tituloRepo) : base(listasRepo, logger)
    {
        _listasRepo = listasRepo;
        _elementosRepo = elementosRepo;
        _logger = logger;
        _tituloRepo = tituloRepo;
    }

    public async Task<bool> AgregarTituloAsync(int listaId, int tituloId)
    {
        try
        {
            _logger.LogInformation("Intentando agregar título {TituloId} a la lista {ListaId}", tituloId, listaId);

           
            var lista = await _listasRepo.GetById(listaId);
            if (lista == null)
            {
                _logger.LogWarning("No se encontró la lista con ID {ListaId}", listaId);
                throw new NotFoundException($"Lista con ID {listaId} no encontrada");
            }

            
            var titulo = await _tituloRepo.GetById(tituloId);
            if (titulo == null)
            {
                _logger.LogWarning("No se encontró el título con ID {TituloId}", tituloId);
                throw new NotFoundException($"Título con ID {tituloId} no encontrado");
            }

          
            var existentes = await _elementosRepo.ListAsync(e =>
                e.ListaSeguimientoId == listaId && e.TituloId == tituloId);

            if (existentes.Any())
            {
                _logger.LogWarning("El título {TituloId} ya está en la lista {ListaId}", tituloId, listaId);
                throw new InvalidOperationException("Este título ya existe en la lista.");
            }

            
            var nuevo = new ElementoLista
            {
                ListaSeguimientoId = listaId,
                TituloId = tituloId,
                AddAt = DateTime.UtcNow
            };

            await _elementosRepo.Add(nuevo);

            _logger.LogInformation("Título {TituloId} agregado correctamente a la lista {ListaId}", tituloId, listaId);
            return true;
        }
        catch (NotFoundException)
        {
            
            throw;
        }
        catch (InvalidOperationException)
        {
            
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al agregar título {TituloId} a la lista {ListaId}", tituloId, listaId);
            throw new Exception($"Error interno al agregar título {tituloId} a la lista {listaId}", ex);
        }
    }


    public async Task<IEnumerable<ListaSeguimiento>> GetByUsuarioAsync(int usuarioId)
    {
        try
        {
            return await _listasRepo.ListAsync(l => l.UserId == usuarioId);
        }
        catch (Exception e)
        {
            _logger.LogError("Error al obtener las Listas de Seguimiento a partir del usuarioId", e.Message);
            throw new Exception("Error interno al obtener listas de seguimiento", e);
        }
    }

    public async Task<bool> EliminarTituloAsync(int listaId, int tituloId)
    {
        var elemento = (await _elementosRepo.ListAsync(e => e.ListaSeguimientoId == listaId && e.TituloId == tituloId))
                       .FirstOrDefault();

        if (elemento == null)
            throw new NotFoundException($"El título con ID {tituloId} no existe en la lista {listaId}");

        await _elementosRepo.Delete(elemento.Id);
        _logger.LogInformation("Título {TituloId} eliminado de la lista {ListaId}", tituloId, listaId);
        return true;
    }
}
