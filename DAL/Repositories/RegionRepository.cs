using DAL.Abstractions;
using DAL.Context;
using DAL.Models;

namespace DAL.Repositories;

public sealed class RegionRepository : GenericRepository<Region>
{
    public RegionRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
