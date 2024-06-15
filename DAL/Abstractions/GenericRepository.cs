using DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace DAL.Abstractions;

public class GenericRepository<T> : IRepository<T> where T : class, new()
{
    private readonly AppDbContext _appDbContext;
    public GenericRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(T entity)
    {
        await _appDbContext.Set<T>().AddAsync(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _appDbContext.Set<T>().Remove(entity);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _appDbContext.Set<T>().Entry(entity).State = EntityState.Modified;
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync() => await _appDbContext.Set<T>().ToListAsync();

    public async Task<T> GetByIdAsync(int id) => await _appDbContext.Set<T>().FindAsync(id);
}
