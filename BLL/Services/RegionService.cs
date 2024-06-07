using AutoMapper;
using BLL.Abstractions;
using BLL.DTO;
using DAL.Abstractions;
using DAL.Models;

namespace BLL.Services;

public sealed class RegionService : GenericService<RegionDTO, Region>
{
    public RegionService(IRepository<Region> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
