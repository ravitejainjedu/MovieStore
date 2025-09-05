using ApplicationCore.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class EfRepository<T> : IRepository<T> where T : class
{
    protected readonly MovieShopDbContext _db;
    public EfRepository(MovieShopDbContext db) => _db = db;

    public async Task<T> AddAsync(T entity)
    {
        _db.Set<T>().Add(entity);
        await _db.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        _db.Set<T>().Remove(entity);
        await _db.SaveChangesAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id) => await _db.Set<T>().FindAsync(id);

    public async Task<IReadOnlyList<T>> ListAllAsync() => await _db.Set<T>().ToListAsync();

    public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>> predicate) =>
        await _db.Set<T>().Where(predicate).ToListAsync();

    public async Task<T> UpdateAsync(T entity)
    {
        _db.Entry(entity).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return entity;
    }
}
