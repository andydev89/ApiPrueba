using ApiPrueba.src.Application.Interfaces;
using ApiPrueba.src.Domain.Entities;
using ApiPrueba.src.Domain.Repositories;

namespace ApiPrueba.src.Application.Services;

using ApiPrueba.src.Domain.Exeptions;
using Microsoft.Extensions.Logging;

public class Service<T, I> : IService<T, I> where T : BaseEntity<I>
{
    private readonly IRepository<T, I> _repository;
    private readonly ILogger<Service<T, I>> _logger;

    public Service(IRepository<T, I> repository, ILogger<Service<T, I>> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            _logger.LogInformation("Obteniendo todos los registros de {EntityName}", typeof(T).Name);
            var result = await _repository.GetAll();
            _logger.LogInformation("Se obtuvieron {Count} registros de {EntityName}", result?.Count() ?? 0, typeof(T).Name);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener todos los registros de {EntityName}", typeof(T).Name);
            throw;
        }
    }

    public async Task<T> GetByIdAsync(I id)
    {
        try
        {
            _logger.LogInformation("Buscando entidad {EntityName} con ID {Id}", typeof(T).Name, id);
            var entity = await _repository.GetById(id);

            if (entity == null)
            {
                _logger.LogWarning("No se encontró entidad {EntityName} con ID {Id}", typeof(T).Name, id);
                throw new NotFoundException($"{typeof(T).Name} con ID {id} no encontrado");
            }

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener {EntityName} con ID {Id}", typeof(T).Name, id);
            throw;
        }
    }

    public async Task<T> CreateAsync(T entity)
    {
        try
        {
            _logger.LogInformation("Creando nueva entidad {EntityName}: {@Entity}", typeof(T).Name, entity);
            var result = await _repository.Add(entity);
            _logger.LogInformation("Entidad {EntityName} creada correctamente con ID {Id}", typeof(T).Name, result.Id);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al crear entidad {EntityName}: {@Entity}", typeof(T).Name, entity);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        try
        {
            _logger.LogInformation("Actualizando entidad {EntityName} con ID {Id}", typeof(T).Name, entity.Id);
            var success = await _repository.Update(entity);
            _logger.LogInformation("Entidad {EntityName} con ID {Id} actualizada correctamente", typeof(T).Name, entity.Id);
            return success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al actualizar entidad {EntityName} con ID {Id}", typeof(T).Name, entity.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(I id)
    {
        try
        {
            _logger.LogInformation("Eliminando entidad {EntityName} con ID {Id}", typeof(T).Name, id);
            var success = await _repository.Delete(id);
            _logger.LogInformation("Entidad {EntityName} con ID {Id} eliminada correctamente", typeof(T).Name, id);
            return success;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al eliminar entidad {EntityName} con ID {Id}", typeof(T).Name, id);
            throw;
        }
    }
}


