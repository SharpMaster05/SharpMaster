namespace DAL.Abstractions;

public interface IRepository<T> where T : class, new()
{
    void Add(T entity);
    void Delete(T entity);
    void Update(T entity);
    T GetById(int id);
    IEnumerable<T> GetAll();
}
