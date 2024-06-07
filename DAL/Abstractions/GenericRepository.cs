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

    public void Add(T entity)
    {
        _appDbContext.Set<T>().Add(entity);
        _appDbContext.SaveChangesAsync();
    }

    public void Delete(T entity)
    {
        _appDbContext.Set<T>().Remove(entity);
        _appDbContext.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        _appDbContext.Set<T>().Entry(entity).State = EntityState.Modified;
        _appDbContext.SaveChangesAsync();
    }

    public IEnumerable<T> GetAll() => _appDbContext.Set<T>().ToList();

    public T GetById(int id) => _appDbContext.Set<T>().Find(id);
}
