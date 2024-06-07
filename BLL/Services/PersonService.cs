using AutoMapper;
using BLL.Abstractions;
using BLL.DTO;
using DAL.Abstractions;
using DAL.Models;

namespace BLL.Services;

public sealed class PersonService : GenericService<PersonDTO, Person>
{
    public PersonService(IRepository<Person> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
