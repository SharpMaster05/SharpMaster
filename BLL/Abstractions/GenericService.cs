using AutoMapper;
using DAL.Abstractions;

namespace BLL.Abstractions;

public class GenericService<DTO, Entity> : IService<DTO> where DTO : class, new() where Entity : class, new()
{
    private readonly IRepository<Entity> _repository;
    private readonly IMapper _mapper;

    public GenericService(IRepository<Entity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task AddAsync(DTO service)
    {
        var entity = _mapper.Map<Entity>(service);
        await _repository.AddAsync(entity);
    }

    public async Task DeleteAsync(DTO service)
    {
        var entity = _mapper.Map<Entity>(service);
        var existingEntity =  await _repository.GetByIdAsync(GetKey(entity));
        
        if (existingEntity != null)
        {
            await _repository.DeleteAsync(existingEntity);
        }
    }

    public async Task UpdateAsync(DTO service)
    {
        var entity = _mapper.Map<Entity>(service);
        var existingEntity = await _repository.GetByIdAsync(GetKey(entity));

        if (existingEntity != null)
        {
            _mapper.Map(service, existingEntity);
            await _repository.UpdateAsync(existingEntity);
        }
    }

    public async Task<IEnumerable<DTO>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(e => _mapper.Map<DTO>(e));
    }

    public async Task<DTO> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<DTO>(entity);
    }

    private int GetKey(Entity entity)
    {
        var propertyInfo = entity.GetType().GetProperty("PersonId");
        if (propertyInfo != null)
        {
            return (int)propertyInfo.GetValue(entity);
        }
        throw new ArgumentException("Entity does not have an Id property");
    }
}
