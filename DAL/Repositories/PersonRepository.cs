using DAL.Abstractions;
using DAL.Context;
using DAL.Models;

namespace DAL.Repositories;

public sealed class PersonRepository : GenericRepository<Person>
{
    public PersonRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
