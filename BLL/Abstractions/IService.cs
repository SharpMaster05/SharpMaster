namespace BLL.Abstractions;

public interface IService<T> where T : class, new()
{
    void Add(T service);
    void Delete(T service);
    void Update(T service);
    T GetById(int id);
    IEnumerable<T> GetAll();
}
