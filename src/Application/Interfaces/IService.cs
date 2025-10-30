namespace ApiPrueba.src.Application.Interfaces;

public interface IService<T, I>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(I id);
    Task<T> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(I id);
}


