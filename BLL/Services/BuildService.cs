using AutoMapper;
using BLL.Abstractions;
using BLL.DTO;
using DAL.Abstractions;
using DAL.Models;

namespace BLL.Services;

public sealed class BuildService : GenericService<BuildDTO, Build>
{
    public BuildService(IRepository<Build> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
