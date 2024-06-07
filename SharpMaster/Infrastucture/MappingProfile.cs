using AutoMapper;
using BLL.DTO;
using DAL.Models;

namespace SharpMaster.Infrastucture;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PersonDTO, Person>().ReverseMap();
        CreateMap<BuildDTO, Build>().ReverseMap();
        CreateMap<RegionDTO, Region>().ReverseMap();
    }
}
