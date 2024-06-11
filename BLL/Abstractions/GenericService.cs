﻿using AutoMapper;
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
        var existingEntity = _repository.GetById(GetKey(entity));
        
        if (existingEntity != null)
        {
            _repository.Delete(existingEntity);
        }
    }

    public void Update(DTO service)
    {
        var entity = _mapper.Map<Entity>(service);
        var existingEntity = _repository.GetById(GetKey(entity));

        if (existingEntity != null)
        {
            _mapper.Map(service, existingEntity);
            _repository.Update(existingEntity);
        }
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
