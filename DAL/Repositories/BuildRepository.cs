using DAL.Abstractions;
using DAL.Context;
using DAL.Models;

namespace DAL.Repositories;

public sealed class BuildRepository : GenericRepository<Build>
{
    public BuildRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
