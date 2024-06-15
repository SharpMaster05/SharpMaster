namespace BLL.Abstractions;

public interface IService<T> where T : class, new()
{
    Task AddAsync(T service);
    Task DeleteAsync(T service);
    Task UpdateAsync(T service);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
}
