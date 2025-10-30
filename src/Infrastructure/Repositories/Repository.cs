using ApiPrueba.src.Domain.Entities;
using ApiPrueba.src.Domain.Exeptions;
using ApiPrueba.src.Domain.Repositories;
using ApiPrueba.src.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiPrueba.src.Infrastructure.Repositories;

 public class Repository<T, I> : IRepository<T, I> where T : BaseEntity<I>
{

    private readonly AppDbContext _db;
    private readonly DbSet<T> _entities;
    private readonly IConfiguration _configuration;
    public Repository(AppDbContext db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
        _entities = db.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAll()
    {
        return await _entities.ToListAsync();
    }
    public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));

        return await _entities.Where(predicate).ToListAsync();
    }
    public async Task<T> GetFisrtByAsync(Expression<Func<T, bool>> predicate)
    {
        if (predicate == null) throw new ArgumentNullException(nameof(predicate));
        var t = await _entities.Where(predicate).FirstOrDefaultAsync();
        if (t == null) throw new NotFoundException("No existe");
        return t;
    }
    public async Task<T> GetById(I id)
    {
        var t = await _entities.FindAsync(id);
        if (t == null) throw new NotFoundException("No existe");
        return t;
    }
    public async Task<T> Add(T entity)
    {
        await _entities.AddAsync(entity);
        await _db.SaveChangesAsync();

        return entity;
    }
    public async Task<bool> Update(T entity)
    {
        await GetById(entity.Id);
        _entities.Update(entity);
        await _db.SaveChangesAsync();

        return true;
    }
    public async Task<bool> Delete(I id)
    {
        var entity = await GetById(id);

        _entities.Remove(entity);
        _db.SaveChanges();
        return true;
    }

   
    public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
    {
        await _entities.AddRangeAsync(entities);
        await _db.SaveChangesAsync();

        return true;
    }

    public async Task DeleteRangeAsync(IEnumerable<T> entities)
    {
        _entities.RemoveRange(entities);
        await _db.SaveChangesAsync();
    }

}
