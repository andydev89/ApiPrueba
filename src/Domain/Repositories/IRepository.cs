using ApiPrueba.src.Domain.Entities;
using System.Linq.Expressions;

namespace ApiPrueba.src.Domain.Repositories;

public interface IRepository<T, I> where T : BaseEntity<I>
{
    Task<IEnumerable<T>> GetAll();//filter
    Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetFisrtByAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetById(I id);
    Task<bool> Update(T entity);
    Task<T> Add(T entity);
    Task<bool> Delete(I id);
    Task<bool> AddRangeAsync(IEnumerable<T> entites);
    Task DeleteRangeAsync(IEnumerable<T> entities);

    Task<(IEnumerable<T> Items, int TotalCount)> ListPagedAsync(
            Expression<Func<T, bool>>? predicate = null,
            int page = 1,
            int pageSize = 10,
            Expression<Func<T, object>>? orderBy = null,
            bool descending = false);
    IQueryable<T> Query();
}
