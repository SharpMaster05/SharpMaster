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

    public void Add(DTO service)
    {
        var entity = _mapper.Map<Entity>(service);
        _repository.Add(entity);
    }

    public void Delete(DTO service)
    {
        var entity = _mapper.Map<Entity>(service);
        _repository.Delete(entity);
    }

    public void Update(DTO service)
    {
        var entity = _mapper.Map<Entity>(service);
        _repository.Update(entity);
    }

    public IEnumerable<DTO> GetAll()
    {
        var entities = _repository.GetAll();
        return entities.Select(e => _mapper.Map<DTO>(e));
    }

    public DTO GetById(int id)
    {
        var entity = _repository.GetById(id);
        return _mapper.Map<DTO>(entity);
    }
}
